namespace InventoryManagement.Application.Interfaces;

public record AuditLog(
    Guid UserId,
    string Email,
    string ActionName,
    DateTime Timestamp
);

public interface IAuditService
{
    Task LogAction(AuditLog auditLog, CancellationToken cancellationToken = default);
}
