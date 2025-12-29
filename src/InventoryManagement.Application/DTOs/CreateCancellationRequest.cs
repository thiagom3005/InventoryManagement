namespace InventoryManagement.Application.DTOs;

public record CreateCancellationRequest
{
    public DateTime? CancelledAt { get; init; }
    public string? Reason { get; init; }
}
