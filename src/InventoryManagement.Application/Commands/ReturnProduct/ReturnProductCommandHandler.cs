using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Commands.ReturnProduct;

public class ReturnProductCommandHandler : IRequestHandler<ReturnProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public ReturnProductCommandHandler(
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

    public async Task Handle(ReturnProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException("Product", request.ProductId);

        product.Return();

        await _unitOfWork.CommitAsync(cancellationToken);

        await _auditService.LogAction(new AuditLog(
            UserId: _currentUserService.UserId ?? Guid.Empty,
            Email: _currentUserService.Email ?? "unknown@system.com",
            ActionName: "ProductReturned",
            Timestamp: DateTime.UtcNow
        ), cancellationToken);
    }
}
