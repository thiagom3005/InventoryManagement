namespace InventoryManagement.Domain.Events;

public record ProductReturnedEvent(Guid ProductId) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
