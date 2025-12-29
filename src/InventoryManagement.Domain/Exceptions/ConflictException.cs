namespace InventoryManagement.Domain.Exceptions;

/// <summary>
/// Exceção para conflitos de estado que devem retornar 409 Conflict
/// </summary>
public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message)
    {
    }
}
