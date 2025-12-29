using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.CancelProduct;

public class CancelProductCommandHandler : IRequestHandler<CancelProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CancelProductCommandHandler(
        IProductRepository productRepository,
        IAuditService auditService,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _auditService = auditService;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CancelProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException("Product", request.ProductId);

        product.Cancel();

        await _unitOfWork.CommitAsync(cancellationToken);

        await _auditService.LogAction(new AuditLog(
            UserId: _currentUserService.UserId ?? Guid.Empty,
            Email: _currentUserService.Email ?? "unknown@system.com",
            ActionName: "ProductCancelled",
            Timestamp: DateTime.UtcNow
        ), cancellationToken);
    }
}
