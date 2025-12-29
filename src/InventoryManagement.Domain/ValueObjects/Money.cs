using InventoryManagement.Domain.Exceptions;

namespace InventoryManagement.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = string.Empty;

    private Money() { }

    public static Money Create(decimal amount, string currency)
    {
        if (amount < 0)
            throw new DomainException("Valor não pode ser negativo");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Moeda é obrigatória");

        return new Money
        {
            Amount = amount,
            Currency = currency.ToUpper()
        };
    }
}
