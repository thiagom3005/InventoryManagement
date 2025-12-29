namespace InventoryManagement.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
