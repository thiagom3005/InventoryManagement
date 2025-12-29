using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    DateTime GetTokenExpiration();
}
