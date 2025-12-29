using InventoryManagement.Application.Commands.CancelProduct;
using InventoryManagement.Application.Commands.CreateProduct;
using InventoryManagement.Application.Commands.ReturnProduct;
using InventoryManagement.Application.Commands.SellProduct;
using InventoryManagement.Application.Common;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Queries.GetCategoryById;
using InventoryManagement.Application.Queries.GetProductById;
using InventoryManagement.Application.Queries.GetProducts;
using InventoryManagement.Application.Queries.GetSupplierById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "ManagerOrAdmin")] // Apenas Manager ou Admin podem criar produtos
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpGet]
    [AllowAnonymous] // Qualquer um pode listar produtos (público)
    [ProducesResponseType(typeof(PagedResult<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous] // Público
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}/supplier")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductSupplier(Guid id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        var supplier = await _mediator.Send(new GetSupplierByIdQuery(product.SupplierId), cancellationToken);
        return Ok(supplier);
    }

    [HttpGet("{id}/category")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductCategory(Guid id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        var category = await _mediator.Send(new GetCategoryByIdQuery(product.CategoryId), cancellationToken);
        return Ok(category);
    }

    [HttpPost("{id}/sales")]
    [Authorize(Policy = "UserOrAbove")] // User, Manager ou Admin podem vender
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateSale(
        Guid id,
        [FromBody] CreateSaleRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new SellProductCommand(id), cancellationToken);
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id }, product);
    }

    [HttpPost("{id}/cancellations")]
    [Authorize(Policy = "UserOrAbove")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateCancellation(
        Guid id,
        [FromBody] CreateCancellationRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelProductCommand(id), cancellationToken);
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id }, product);
    }

    [HttpPost("{id}/returns")]
    [Authorize(Policy = "UserOrAbove")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateReturn(
        Guid id,
        [FromBody] CreateReturnRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReturnProductCommand(id), cancellationToken);
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id }, product);
    }
}
