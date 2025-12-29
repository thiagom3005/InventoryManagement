using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Commands.CreateSupplier;

public record CreateSupplierCommand(
    string Name,
    string Email,
    string Currency,
    string Country
) : IRequest<SupplierResponse>;
