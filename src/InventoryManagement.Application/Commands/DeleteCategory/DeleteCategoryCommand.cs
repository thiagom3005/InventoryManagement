using MediatR;

namespace InventoryManagement.Application.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
