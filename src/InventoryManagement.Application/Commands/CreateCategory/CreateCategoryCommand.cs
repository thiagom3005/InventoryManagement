using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Shortcode,
    Guid? ParentCategoryId
) : IRequest<CategoryResponse>;
