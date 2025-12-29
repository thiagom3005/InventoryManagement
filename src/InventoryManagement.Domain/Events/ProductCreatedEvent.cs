namespace InventoryManagement.Domain.Events;

public record ProductCreatedEvent(Guid ProductId) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
