using InventoryManagement.Application.Common;

namespace InventoryManagement.Application.DTOs;

public record SupplierResponse(
    Guid Id,
    string Name,
    string Email,
    string Currency,
    string Country
)
{
    public IReadOnlyList<Link> Links { get; init; } = new List<Link>();
};
