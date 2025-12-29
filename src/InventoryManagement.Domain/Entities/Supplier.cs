using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.ValueObjects;

namespace InventoryManagement.Domain.Entities;

public class Supplier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public string Currency { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    private Supplier() { } // EF Core

    public static Supplier Create(string name, string email, string currency, string country)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome do fornecedor é obrigatório");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Moeda é obrigatória");

        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("País é obrigatório");

        return new Supplier
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = Email.Create(email),
            Currency = currency.Trim().ToUpper(),
            Country = country.Trim()
        };
    }
}
