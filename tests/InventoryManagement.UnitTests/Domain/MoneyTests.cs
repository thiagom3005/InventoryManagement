using FluentAssertions;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.ValueObjects;

namespace InventoryManagement.UnitTests.Domain;

public class MoneyTests
{
    [Fact]
    public void Create_WithValidAmount_ShouldCreateMoney()
    {
        // Act
        var money = Money.Create(100.50m, "USD");

        // Assert
        money.Amount.Should().Be(100.50m);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Create_WithNegativeAmount_ShouldThrowDomainException()
    {
        // Act
        Action act = () => Money.Create(-10, "USD");

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Valor não pode ser negativo");
    }

    [Theory]
    [InlineData("brl", "BRL")]
    [InlineData("eur", "EUR")]
    [InlineData("Usd", "USD")]
    public void Create_ShouldNormalizeCurrencyToUpperCase(string input, string expected)
    {
        // Act
        var money = Money.Create(100, input);

        // Assert
        money.Currency.Should().Be(expected);
    }

    [Fact]
    public void Create_WithZeroAmount_ShouldCreateMoney()
    {
        // Act
        var money = Money.Create(0, "USD");

        // Assert
        money.Amount.Should().Be(0);
    }

    [Fact]
    public void Create_WithEmptyCurrency_ShouldThrowDomainException()
    {
        // Act
        Action act = () => Money.Create(100, "");

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Moeda é obrigatória");
    }
}
