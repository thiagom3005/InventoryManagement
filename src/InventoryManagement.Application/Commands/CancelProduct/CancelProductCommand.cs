using MediatR;

namespace InventoryManagement.Application.Commands.CancelProduct;

public record CancelProductCommand(Guid ProductId) : IRequest;
