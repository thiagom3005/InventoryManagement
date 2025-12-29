using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<PagedResult<CategoryResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Name { get; init; }
    public string? Shortcode { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public bool? HasParent { get; init; }
    public string? OrderBy { get; init; }
    public bool Descending { get; init; } = false;
}
