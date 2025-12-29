using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly ApplicationDbContext _context;

    public SupplierRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Suppliers.ToListAsync(cancellationToken);
    }

    public Task<IQueryable<Supplier>> GetQueryableAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.Suppliers.AsQueryable();
        return Task.FromResult(query);
    }

    public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        await _context.Suppliers.AddAsync(supplier, cancellationToken);
    }

    public Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        _context.Suppliers.Remove(supplier);
        return Task.CompletedTask;
    }

    public async Task<bool> HasProductsAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(p => p.SupplierId == supplierId, cancellationToken);
    }
}
