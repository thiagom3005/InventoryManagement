using MediatR;

namespace InventoryManagement.Application.Commands.DeleteSupplier;

public record DeleteSupplierCommand(Guid Id) : IRequest<Unit>;
