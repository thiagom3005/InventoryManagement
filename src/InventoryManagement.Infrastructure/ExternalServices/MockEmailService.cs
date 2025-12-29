using InventoryManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Infrastructure.ExternalServices;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;

    public MockEmailService(ILogger<MockEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendProductSoldNotification(Guid productId, string supplierEmail, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "[EMAIL MOCK] Notificação de venda enviada para {Email} - Produto: {ProductId}",
            supplierEmail, productId);

        return Task.Delay(50, cancellationToken);
    }
}
