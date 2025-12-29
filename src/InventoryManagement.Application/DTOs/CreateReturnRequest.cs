namespace InventoryManagement.Application.DTOs;

public record CreateReturnRequest
{
    public DateTime? ReturnedAt { get; init; }
    public string? Reason { get; init; }
}
