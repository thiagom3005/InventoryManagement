namespace InventoryManagement.Application.DTOs.Auth;

public record RegisterRequest(
    string Username,
    string Email,
    string Password,
    string FullName,
    string Role = "User" // Default role
);
