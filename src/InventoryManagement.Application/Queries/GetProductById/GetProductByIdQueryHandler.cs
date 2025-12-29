using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Product", request.Id);

        var links = HateoasLinkGenerator.GenerateProductLinks(product.Id, product.Status);

        return new ProductResponse(
            product.Id,
            product.SupplierId,
            product.Supplier.Name,
            product.CategoryId,
            product.Category.Name,
            product.Description,
            new MoneyDto(product.AcquisitionCost.Amount, product.AcquisitionCost.Currency),
            new MoneyDto(product.AcquisitionCostUsd.Amount, product.AcquisitionCostUsd.Currency),
            product.AcquisitionDate,
            product.SaleDate,
            product.CancellationDate,
            product.ReturnDate,
            product.Status.ToString()
        )
        {
            Links = links
        };
    }
}
