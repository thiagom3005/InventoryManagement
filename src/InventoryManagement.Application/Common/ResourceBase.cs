namespace InventoryManagement.Application.Common;

public abstract record ResourceBase
{
    public IReadOnlyList<Link> Links { get; init; } = new List<Link>();
}
