using InventoryManagement.Application.DTOs;
using MediatR;

namespace InventoryManagement.Application.Commands.CreateProduct;

public record CreateProductCommand(
    Guid SupplierId,
    Guid CategoryId,
    string Description,
    decimal AcquisitionCost,
    string AcquisitionCurrency,
    decimal AcquisitionCostUsd
) : IRequest<ProductResponse>;
