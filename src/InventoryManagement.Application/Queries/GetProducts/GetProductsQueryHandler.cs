using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedResult<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = await _productRepository.GetQueryableAsync(cancellationToken);

        // Aplicar filtros
        if (request.SupplierId.HasValue)
            query = query.Where(p => p.SupplierId == request.SupplierId.Value);

        if (request.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);

        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status.Value);

        if (!string.IsNullOrWhiteSpace(request.Description))
            query = query.Where(p => p.Description.Contains(request.Description));

        if (request.AcquisitionDateFrom.HasValue)
            query = query.Where(p => p.AcquisitionDate >= request.AcquisitionDateFrom.Value);

        if (request.AcquisitionDateTo.HasValue)
            query = query.Where(p => p.AcquisitionDate <= request.AcquisitionDateTo.Value);

        // Contar total antes da paginação
        var totalCount = await query.CountAsync(cancellationToken);

        // Aplicar ordenação
        query = ApplyOrdering(query, request.OrderBy, request.Descending);

        // Aplicar paginação
        var products = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var productResponses = products.Select(p =>
        {
            var links = HateoasLinkGenerator.GenerateProductLinks(p.Id, p.Status);

            return new ProductResponse(
                p.Id,
                p.SupplierId,
                p.Supplier.Name,
                p.CategoryId,
                p.Category.Name,
                p.Description,
                new MoneyDto(p.AcquisitionCost.Amount, p.AcquisitionCost.Currency),
                new MoneyDto(p.AcquisitionCostUsd.Amount, p.AcquisitionCostUsd.Currency),
                p.AcquisitionDate,
                p.SaleDate,
                p.CancellationDate,
                p.ReturnDate,
                p.Status.ToString()
            )
            {
                Links = links
            };
        }).ToList();

        return PagedResult<ProductResponse>.Create(
            productResponses,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }

    private static IQueryable<Domain.Entities.Product> ApplyOrdering(
        IQueryable<Domain.Entities.Product> query,
        string? orderBy,
        bool descending)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return descending
                ? query.OrderByDescending(p => p.AcquisitionDate)
                : query.OrderBy(p => p.AcquisitionDate);

        return orderBy.ToLowerInvariant() switch
        {
            "description" => descending
                ? query.OrderByDescending(p => p.Description)
                : query.OrderBy(p => p.Description),
            "status" => descending
                ? query.OrderByDescending(p => p.Status)
                : query.OrderBy(p => p.Status),
            "acquisitiondate" => descending
                ? query.OrderByDescending(p => p.AcquisitionDate)
                : query.OrderBy(p => p.AcquisitionDate),
            _ => descending
                ? query.OrderByDescending(p => p.AcquisitionDate)
                : query.OrderBy(p => p.AcquisitionDate)
        };
    }
}
