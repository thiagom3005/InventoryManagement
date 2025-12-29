using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.SellProduct;

public class SellProductCommandHandler : IRequestHandler<SellProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IWmsService _wmsService;
    private readonly IAuditService _auditService;
    private readonly IEmailService _emailService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public SellProductCommandHandler(
        IProductRepository productRepository,
        IWmsService wmsService,
        IAuditService auditService,
        IEmailService emailService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _wmsService = wmsService;
        _auditService = auditService;
        _emailService = emailService;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SellProductCommand request, CancellationToken cancellationToken)
    {
        // Buscar produto
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException("Product", request.ProductId);

        // Regra de negócio
        product.Sell();

        // Persistir
        await _unitOfWork.CommitAsync(cancellationToken);

        // Integrações
        await Task.WhenAll(
            _wmsService.DispatchProduct(product.Id, cancellationToken),
            _emailService.SendProductSoldNotification(product.Id, product.Supplier.Email.Value, cancellationToken),
            _auditService.LogAction(new AuditLog(
                UserId: _currentUserService.UserId ?? Guid.Empty,
                Email: _currentUserService.Email ?? "unknown@system.com",
                ActionName: "ProductSold",
                Timestamp: DateTime.UtcNow
            ), cancellationToken)
        );
    }
}
