using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
            throw new NotFoundException($"Categoria com ID {request.Id} não encontrada");

        // Validar se categoria tem produtos
        if (await _categoryRepository.HasProductsAsync(request.Id, cancellationToken))
            throw new BusinessRuleException("Não é possível excluir categoria que possui produtos associados");

        // Validar se categoria tem subcategorias
        if (await _categoryRepository.HasSubCategoriesAsync(request.Id, cancellationToken))
            throw new BusinessRuleException("Não é possível excluir categoria que possui subcategorias");

        await _categoryRepository.DeleteAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
