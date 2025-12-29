namespace InventoryManagement.Application.DTOs;

public record CreateSaleRequest
{
    public DateTime? SoldAt { get; init; }
}
