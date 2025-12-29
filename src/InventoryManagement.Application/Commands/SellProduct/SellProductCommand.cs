using MediatR;

namespace InventoryManagement.Application.Commands.SellProduct;

public record SellProductCommand(Guid ProductId) : IRequest;
