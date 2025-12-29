using InventoryManagement.Domain.Exceptions;

namespace InventoryManagement.Domain.ValueObjects;

public record Email
{
    public string Value { get; init; } = string.Empty;

    private Email() { }

    public static Email Create(string value)
    {
        if (!IsValid(value))
            throw new DomainException("Email inválido");

        return new Email { Value = value.ToLowerInvariant() };
    }

    private static bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        if (email.Length < 3)
            return false;

        var parts = email.Split('@');
        if (parts.Length != 2)
            return false;

        return !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]);
    }
}
