using InventoryManagement.Application.Common;

namespace InventoryManagement.Application.DTOs;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Shortcode,
    Guid? ParentCategoryId,
    string? ParentCategoryName
)
{
    public IReadOnlyList<Link> Links { get; init; } = new List<Link>();
};
