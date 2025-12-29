using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Queries.GetProducts;

public record GetProductsQuery : IRequest<PagedResult<ProductResponse>>
{
    // Paginação
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    // Filtros
    public Guid? SupplierId { get; init; }
    public Guid? CategoryId { get; init; }
    public ProductStatus? Status { get; init; }
    public string? Description { get; init; }
    public DateTime? AcquisitionDateFrom { get; init; }
    public DateTime? AcquisitionDateTo { get; init; }

    // Ordenação
    public string? OrderBy { get; init; } // "acquisitionDate", "description", "status"
    public bool Descending { get; init; } = false;
}
