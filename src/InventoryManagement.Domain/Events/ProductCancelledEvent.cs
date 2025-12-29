namespace InventoryManagement.Domain.Events;

public record ProductCancelledEvent(Guid ProductId) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
