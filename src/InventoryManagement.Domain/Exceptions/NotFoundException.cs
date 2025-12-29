namespace InventoryManagement.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} com ID '{key}' não foi encontrado")
    {
    }
}
