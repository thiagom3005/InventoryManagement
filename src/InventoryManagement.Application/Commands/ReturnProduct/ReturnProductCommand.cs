using MediatR;

namespace InventoryManagement.Application.Commands.ReturnProduct;

public record ReturnProductCommand(Guid ProductId) : IRequest;
