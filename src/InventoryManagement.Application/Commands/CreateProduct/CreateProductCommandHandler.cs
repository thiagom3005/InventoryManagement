using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Domain.ValueObjects;
using MediatR;

namespace InventoryManagement.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWmsService _wmsService;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        ISupplierRepository supplierRepository,
        ICategoryRepository categoryRepository,
        IWmsService wmsService,
        IAuditService auditService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
        _categoryRepository = categoryRepository;
        _wmsService = wmsService;
        _auditService = auditService;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validar se fornecedor existe
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken)
            ?? throw new NotFoundException("Supplier", request.SupplierId);

        // Validar se categoria existe
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new NotFoundException("Category", request.CategoryId);

        // Criar produto no domínio
        var product = Product.Create(
            request.SupplierId,
            request.CategoryId,
            request.Description,
            Money.Create(request.AcquisitionCost, request.AcquisitionCurrency),
            Money.Create(request.AcquisitionCostUsd, "USD")
        );

        // Persistir
        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        // Integrações assíncronas
        await Task.WhenAll(
            _wmsService.CreateProductInWarehouse(product.Id, product.Description, cancellationToken),
            _auditService.LogAction(new AuditLog(
                UserId: _currentUserService.UserId ?? Guid.Empty,
                Email: _currentUserService.Email ?? "unknown@system.com",
                ActionName: "ProductCreated",
                Timestamp: DateTime.UtcNow
            ), cancellationToken)
        );

        return MapToResponse(product, supplier, category);
    }

    private static ProductResponse MapToResponse(Product product, Supplier supplier, Category category)
    {
        var links = HateoasLinkGenerator.GenerateProductLinks(product.Id, product.Status);

        return new ProductResponse(
            product.Id,
            product.SupplierId,
            supplier.Name,
            product.CategoryId,
            category.Name,
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
