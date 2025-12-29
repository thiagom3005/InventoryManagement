using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Events;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.ValueObjects;

namespace InventoryManagement.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public Guid SupplierId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public Money AcquisitionCost { get; private set; } = null!;
    public Money AcquisitionCostUsd { get; private set; } = null!;
    public DateTime AcquisitionDate { get; private set; }
    public DateTime? SaleDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public ProductStatus Status { get; private set; }

    // Navigation properties
    public Supplier Supplier { get; private set; } = null!;
    public Category Category { get; private set; } = null!;

    // Domain Events
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Product() { } // EF Core

    public static Product Create(
        Guid supplierId,
        Guid categoryId,
        string description,
        Money acquisitionCost,
        Money acquisitionCostUsd)
    {
        if (supplierId == Guid.Empty)
            throw new DomainException("Fornecedor é obrigatório");

        if (categoryId == Guid.Empty)
            throw new DomainException("Categoria é obrigatória");

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Descrição é obrigatória");

        if (acquisitionCost == null)
            throw new DomainException("Custo de aquisição é obrigatório");

        if (acquisitionCostUsd == null)
            throw new DomainException("Custo de aquisição em USD é obrigatório");

        if (acquisitionCostUsd.Currency != "USD")
            throw new DomainException("Custo em USD deve estar na moeda USD");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            CategoryId = categoryId,
            Description = description.Trim(),
            AcquisitionCost = acquisitionCost,
            AcquisitionCostUsd = acquisitionCostUsd,
            AcquisitionDate = DateTime.UtcNow,
            Status = ProductStatus.Created
        };

        product.AddDomainEvent(new ProductCreatedEvent(product.Id));
        return product;
    }

    public void Sell()
    {
        if (Status == ProductStatus.Cancelled)
            throw new ConflictException("Produtos cancelados não podem ser vendidos");

        if (Status == ProductStatus.Returned)
            throw new ConflictException("Produtos devolvidos não podem ser vendidos");

        if (Status == ProductStatus.Sold)
            throw new ConflictException("Produto já foi vendido");

        Status = ProductStatus.Sold;
        SaleDate = DateTime.UtcNow;
        AddDomainEvent(new ProductSoldEvent(Id, SupplierId));
    }

    public void Cancel()
    {
        if (Status != ProductStatus.Sold)
            throw new ConflictException("Apenas produtos vendidos podem ser cancelados");

        Status = ProductStatus.Cancelled;
        CancellationDate = DateTime.UtcNow;
        AddDomainEvent(new ProductCancelledEvent(Id));
    }

    public void Return()
    {
        if (Status != ProductStatus.Sold)
            throw new ConflictException("Apenas produtos vendidos podem ser devolvidos");

        Status = ProductStatus.Returned;
        ReturnDate = DateTime.UtcNow;
        AddDomainEvent(new ProductReturnedEvent(Id));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
