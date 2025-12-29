using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<PagedResult<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = await _categoryRepository.GetQueryableAsync(cancellationToken);

        // Aplicar filtros
        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(c => c.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Shortcode))
            query = query.Where(c => c.Shortcode.Contains(request.Shortcode));

        if (request.ParentCategoryId.HasValue)
            query = query.Where(c => c.ParentCategoryId == request.ParentCategoryId.Value);

        if (request.HasParent.HasValue)
        {
            if (request.HasParent.Value)
                query = query.Where(c => c.ParentCategoryId != null);
            else
                query = query.Where(c => c.ParentCategoryId == null);
        }

        // Contar total antes da paginação
        var totalCount = await query.CountAsync(cancellationToken);

        // Aplicar ordenação
        query = ApplyOrdering(query, request.OrderBy, request.Descending);

        // Aplicar paginação
        var categories = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var categoryResponses = categories.Select(c => new CategoryResponse(
            c.Id,
            c.Name,
            c.Shortcode,
            c.ParentCategoryId,
            c.ParentCategory?.Name
        )
        {
            Links = HateoasLinkGenerator.GenerateCategoryLinks(c.Id)
        }).ToList();

        return PagedResult<CategoryResponse>.Create(
            categoryResponses,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }

    private static IQueryable<Domain.Entities.Category> ApplyOrdering(
        IQueryable<Domain.Entities.Category> query,
        string? orderBy,
        bool descending)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return descending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name);

        return orderBy.ToLowerInvariant() switch
        {
            "name" => descending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name),
            "shortcode" => descending
                ? query.OrderByDescending(c => c.Shortcode)
                : query.OrderBy(c => c.Shortcode),
            _ => descending
                ? query.OrderByDescending(c => c.Name)
                : query.OrderBy(c => c.Name)
        };
    }
}
