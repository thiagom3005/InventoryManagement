using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Queries.GetSuppliers;

public record GetSuppliersQuery : IRequest<PagedResult<SupplierResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Currency { get; init; }
    public string? Country { get; init; }
    public string? OrderBy { get; init; }
    public bool Descending { get; init; } = false;
}
