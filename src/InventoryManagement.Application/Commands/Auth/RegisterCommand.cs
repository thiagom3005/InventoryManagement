using InventoryManagement.Application.DTOs.Auth;
using MediatR;

namespace InventoryManagement.Application.Commands.Auth;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string FullName,
    string Role = "User"
) : IRequest<AuthResponse>;
