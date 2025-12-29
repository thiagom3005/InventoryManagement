namespace InventoryManagement.Domain.Exceptions;

/// <summary>
/// Exceção para violações de regras de negócio que devem retornar 422 Unprocessable Entity
/// </summary>
public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message)
    {
    }
}
