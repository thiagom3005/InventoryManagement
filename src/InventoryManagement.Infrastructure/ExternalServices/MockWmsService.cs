using InventoryManagement.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Infrastructure.ExternalServices;

public class MockWmsService : IWmsService
{
    private readonly ILogger<MockWmsService> _logger;

    public MockWmsService(ILogger<MockWmsService> logger)
    {
        _logger = logger;
    }

    public async Task CreateProductInWarehouse(Guid productId, string description, CancellationToken cancellationToken = default)
    {
        // Simula chamada HTTP POST https://api.wms.com/products
        // Body: { "productId": "guid", "description": "...", "categoryShortcode": "...", "supplierId": "..." }
        _logger.LogInformation(
            "[WMS MOCK] POST https://api.wms.com/products - Creating product in warehouse\n" +
            "  ProductId: {ProductId}\n" +
            "  Description: {Description}",
            productId, description);

        // Simula latência de rede (100-200ms)
        await Task.Delay(100, cancellationToken);

        // Simula resposta 201 Created
        var wmsProductId = Guid.NewGuid();
        _logger.LogInformation(
            "[WMS MOCK] Response 201 Created - WMS Product ID: {WmsProductId}",
            wmsProductId);
    }

    public async Task DispatchProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        // Simula chamada HTTP POST https://api.wms.com/products/{productId}/dispatch
        _logger.LogInformation(
            "[WMS MOCK] POST https://api.wms.com/products/{ProductId}/dispatch - Triggering dispatch process",
            productId);

        // Simula latência de rede (100-200ms)
        await Task.Delay(100, cancellationToken);

        // Simula resposta 200 OK
        _logger.LogInformation(
            "[WMS MOCK] Response 200 OK - Product dispatch successfully triggered for {ProductId}",
            productId);
    }
}
