namespace InventoryManagement.Application.Interfaces;

public interface IWmsService
{
    Task CreateProductInWarehouse(Guid productId, string description, CancellationToken cancellationToken = default);
    Task DispatchProduct(Guid productId, CancellationToken cancellationToken = default);
}
