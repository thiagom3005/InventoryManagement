using FluentValidation;

namespace InventoryManagement.Application.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.SupplierId)
            .NotEmpty()
            .WithMessage("Fornecedor é obrigatório");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Categoria é obrigatória");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Descrição é obrigatória")
            .MaximumLength(500)
            .WithMessage("Descrição não pode ter mais de 500 caracteres");

        RuleFor(x => x.AcquisitionCost)
            .GreaterThan(0)
            .WithMessage("Custo de aquisição deve ser maior que zero");

        RuleFor(x => x.AcquisitionCostUsd)
            .GreaterThan(0)
            .WithMessage("Custo de aquisição em USD deve ser maior que zero");

        RuleFor(x => x.AcquisitionCurrency)
            .NotEmpty()
            .WithMessage("Moeda é obrigatória")
            .Length(3)
            .WithMessage("Moeda deve ter 3 caracteres");
    }
}
