using InventoryManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Infrastructure.ExternalServices;

public class MockAuditService : IAuditService
{
    private readonly ILogger<MockAuditService> _logger;

    public MockAuditService(ILogger<MockAuditService> logger)
    {
        _logger = logger;
    }

    public async Task LogAction(AuditLog auditLog, CancellationToken cancellationToken = default)
    {
        // Simula chamada HTTP POST https://api.auditlog.com/logs
        // Body: { "userId": "guid", "email": "user@example.com", "actionName": "PRODUCT_CREATED", "timestamp": "2025-12-26T15:30:00Z" }
        _logger.LogInformation(
            "[AUDIT MOCK] POST https://api.auditlog.com/logs - Creating audit log entry\n" +
            "  UserId: {UserId}\n" +
            "  Email: {Email}\n" +
            "  ActionName: {ActionName}\n" +
            "  Timestamp: {Timestamp}",
            auditLog.UserId, auditLog.Email, auditLog.ActionName, auditLog.Timestamp);

        // Simula latência de rede (50ms - auditoria deve ser rápida)
        await Task.Delay(50, cancellationToken);

        // Simula resposta 201 Created
        _logger.LogInformation(
            "[AUDIT MOCK] Response 201 Created - Audit log entry successfully created for action '{ActionName}' by user {UserId}",
            auditLog.ActionName, auditLog.UserId);
    }
}
