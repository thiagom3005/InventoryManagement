using System.Net;
using System.Net.Mail;
using InventoryManagement.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly ILogger<SmtpEmailService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly string _fromEmail;
    private readonly string _fromName;
    private readonly bool _enableSsl;

    public SmtpEmailService(
        ILogger<SmtpEmailService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        _smtpHost = configuration["Email:Smtp:Host"] ?? "smtp.gmail.com";
        _smtpPort = int.Parse(configuration["Email:Smtp:Port"] ?? "587");
        _smtpUsername = configuration["Email:Smtp:Username"] ?? "";
        _smtpPassword = configuration["Email:Smtp:Password"] ?? "";
        _fromEmail = configuration["Email:From:Email"] ?? "noreply@inventory.com";
        _fromName = configuration["Email:From:Name"] ?? "Inventory Management System";
        _enableSsl = bool.Parse(configuration["Email:Smtp:EnableSsl"] ?? "true");
    }

    public async Task SendProductSoldNotification(Guid productId, string supplierEmail, CancellationToken cancellationToken = default)
    {
        try
        {
            var subject = "🎉 Produto Vendido - Notificação de Venda";
            var body = BuildProductSoldEmailBody(productId);

            await SendEmailAsync(supplierEmail, subject, body, isHtml: true, cancellationToken);

            _logger.LogInformation(
                "[EMAIL SMTP] Email de notificação de venda enviado com sucesso para {Email} (Produto: {ProductId})",
                supplierEmail, productId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "[EMAIL SMTP] Erro ao enviar email de notificação de venda para {Email} (Produto: {ProductId})",
                supplierEmail, productId);

            // Não propaga exceção para não quebrar o fluxo principal
            // Em produção, considere usar filas (RabbitMQ, Azure Service Bus)
        }
    }

    private async Task SendEmailAsync(
        string toEmail,
        string subject,
        string body,
        bool isHtml = true,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_smtpUsername) || string.IsNullOrEmpty(_smtpPassword))
        {
            _logger.LogWarning(
                "[EMAIL SMTP] Credenciais SMTP não configuradas. Email não enviado para {Email}. " +
                "Configure Email:Smtp:Username e Email:Smtp:Password no appsettings.json",
                toEmail);
            return;
        }

        using var message = new MailMessage
        {
            From = new MailAddress(_fromEmail, _fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        message.To.Add(new MailAddress(toEmail));

        using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = _enableSsl,
            Timeout = 10000 // 10 segundos
        };

        await smtpClient.SendMailAsync(message, cancellationToken);
    }

    private string BuildProductSoldEmailBody(Guid productId)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
        .content {{ background-color: #f9f9f9; padding: 30px; border: 1px solid #ddd; }}
        .footer {{ background-color: #333; color: white; padding: 15px; text-align: center; font-size: 12px; border-radius: 0 0 5px 5px; }}
        .highlight {{ background-color: #fff3cd; padding: 15px; border-left: 4px solid #ffc107; margin: 20px 0; }}
        .button {{ display: inline-block; padding: 12px 24px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 4px; margin: 10px 0; }}
        .product-id {{ font-family: monospace; background-color: #e9ecef; padding: 5px 10px; border-radius: 3px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎉 Produto Vendido!</h1>
        </div>
        <div class='content'>
            <h2>Notificação de Venda</h2>
            <p>Olá,</p>
            <p>Informamos que um produto fornecido por você foi <strong>vendido com sucesso</strong> em nosso sistema.</p>

            <div class='highlight'>
                <strong>ID do Produto:</strong><br>
                <span class='product-id'>{productId}</span>
            </div>

            <p><strong>Próximos passos:</strong></p>
            <ul>
                <li>O produto será despachado pelo nosso sistema de armazém (WMS)</li>
                <li>Você receberá atualizações sobre o status do envio</li>
                <li>O pagamento será processado conforme acordado</li>
            </ul>

            <p>Se você tiver alguma dúvida, entre em contato conosco.</p>

            <p style='margin-top: 30px;'>
                Atenciosamente,<br>
                <strong>Inventory Management System</strong>
            </p>
        </div>
        <div class='footer'>
            © {DateTime.UtcNow.Year} Inventory Management System. Todos os direitos reservados.<br>
            Este é um email automático. Por favor, não responda.
        </div>
    </div>
</body>
</html>";
    }
}
