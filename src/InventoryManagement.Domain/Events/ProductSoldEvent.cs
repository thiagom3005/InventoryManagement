namespace InventoryManagement.Domain.Events;

public record ProductSoldEvent(Guid ProductId, Guid SupplierId) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
