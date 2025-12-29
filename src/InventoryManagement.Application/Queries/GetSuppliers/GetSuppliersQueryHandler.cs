using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Queries.GetSuppliers;

public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PagedResult<SupplierResponse>>
{
    private readonly ISupplierRepository _supplierRepository;

    public GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<PagedResult<SupplierResponse>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
    {
        var query = await _supplierRepository.GetQueryableAsync(cancellationToken);

        // Aplicar filtros
        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(s => s.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(s => s.Email.Value.Contains(request.Email));

        if (!string.IsNullOrWhiteSpace(request.Currency))
            query = query.Where(s => s.Currency.Contains(request.Currency));

        if (!string.IsNullOrWhiteSpace(request.Country))
            query = query.Where(s => s.Country.Contains(request.Country));

        // Contar total antes da paginação
        var totalCount = await query.CountAsync(cancellationToken);

        // Aplicar ordenação
        query = ApplyOrdering(query, request.OrderBy, request.Descending);

        // Aplicar paginação
        var suppliers = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var supplierResponses = suppliers.Select(s => new SupplierResponse(
            s.Id,
            s.Name,
            s.Email.Value,
            s.Currency,
            s.Country
        )
        {
            Links = HateoasLinkGenerator.GenerateSupplierLinks(s.Id)
        }).ToList();

        return PagedResult<SupplierResponse>.Create(
            supplierResponses,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }

    private static IQueryable<Domain.Entities.Supplier> ApplyOrdering(
        IQueryable<Domain.Entities.Supplier> query,
        string? orderBy,
        bool descending)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return descending
                ? query.OrderByDescending(s => s.Name)
                : query.OrderBy(s => s.Name);

        return orderBy.ToLowerInvariant() switch
        {
            "name" => descending
                ? query.OrderByDescending(s => s.Name)
                : query.OrderBy(s => s.Name),
            "email" => descending
                ? query.OrderByDescending(s => s.Email.Value)
                : query.OrderBy(s => s.Email.Value),
            "currency" => descending
                ? query.OrderByDescending(s => s.Currency)
                : query.OrderBy(s => s.Currency),
            "country" => descending
                ? query.OrderByDescending(s => s.Country)
                : query.OrderBy(s => s.Country),
            _ => descending
                ? query.OrderByDescending(s => s.Name)
                : query.OrderBy(s => s.Name)
        };
    }
}
