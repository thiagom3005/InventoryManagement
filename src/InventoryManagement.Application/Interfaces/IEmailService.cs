namespace InventoryManagement.Application.Interfaces;

public interface IEmailService
{
    Task SendProductSoldNotification(Guid productId, string supplierEmail, CancellationToken cancellationToken = default);
}
