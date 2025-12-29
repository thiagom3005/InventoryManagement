using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, request.Shortcode, request.ParentCategoryId);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        // Reload para pegar a parent category se existir
        var savedCategory = await _categoryRepository.GetByIdAsync(category.Id, cancellationToken);

        return new CategoryResponse(
            savedCategory!.Id,
            savedCategory.Name,
            savedCategory.Shortcode,
            savedCategory.ParentCategoryId,
            savedCategory.ParentCategory?.Name
        );
    }
}
