namespace InventoryManagement.Application.DTOs.Auth;

public record AuthResponse(
    string Token,
    string Username,
    string Email,
    string FullName,
    string Role,
    DateTime ExpiresAt
);
