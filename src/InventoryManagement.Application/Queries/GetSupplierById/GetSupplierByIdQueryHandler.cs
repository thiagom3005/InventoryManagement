using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierResponse>
{
    private readonly ISupplierRepository _supplierRepository;

    public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SupplierResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Supplier", request.Id);

        var links = HateoasLinkGenerator.GenerateSupplierLinks(supplier.Id);

        return new SupplierResponse(
            supplier.Id,
            supplier.Name,
            supplier.Email.Value,
            supplier.Currency,
            supplier.Country
        )
        {
            Links = links
        };
    }
}
