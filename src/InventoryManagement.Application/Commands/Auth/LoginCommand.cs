using InventoryManagement.Application.DTOs.Auth;
using MediatR;

namespace InventoryManagement.Application.Commands.Auth;

public record LoginCommand(
    string Username,
    string Password
) : IRequest<AuthResponse>;
