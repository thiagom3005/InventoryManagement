using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IQueryable<Supplier>> GetQueryableAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken = default);
    Task<bool> HasProductsAsync(Guid supplierId, CancellationToken cancellationToken = default);
}
