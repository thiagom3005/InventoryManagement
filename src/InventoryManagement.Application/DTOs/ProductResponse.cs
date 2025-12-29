using InventoryManagement.Application.Common;

namespace InventoryManagement.Application.DTOs;

public record ProductResponse(
    Guid Id,
    Guid SupplierId,
    string SupplierName,
    Guid CategoryId,
    string CategoryName,
    string Description,
    MoneyDto AcquisitionCost,
    MoneyDto AcquisitionCostUsd,
    DateTime AcquisitionDate,
    DateTime? SaleDate,
    DateTime? CancellationDate,
    DateTime? ReturnDate,
    string Status
)
{
    public IReadOnlyList<Link> Links { get; init; } = new List<Link>();
}

public record MoneyDto(decimal Amount, string Currency);
