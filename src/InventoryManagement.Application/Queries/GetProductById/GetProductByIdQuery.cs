using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
