using FluentAssertions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Events;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.ValueObjects;

namespace InventoryManagement.UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var supplierId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var cost = Money.Create(100, "BRL");
        var costUsd = Money.Create(20, "USD");

        // Act
        var product = Product.Create(supplierId, categoryId, "Notebook Dell", cost, costUsd);

        // Assert
        product.Should().NotBeNull();
        product.Id.Should().NotBeEmpty();
        product.Status.Should().Be(ProductStatus.Created);
        product.Description.Should().Be("Notebook Dell");
        product.AcquisitionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        product.DomainEvents.Should().ContainSingle(e => e is ProductCreatedEvent);
    }

    [Fact]
    public void Create_WithEmptyDescription_ShouldThrowDomainException()
    {
        // Arrange
        var supplierId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var cost = Money.Create(100, "BRL");
        var costUsd = Money.Create(20, "USD");

        // Act
        Action act = () => Product.Create(supplierId, categoryId, "", cost, costUsd);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Descrição é obrigatória");
    }

    [Fact]
    public void Create_WithInvalidCurrency_ShouldThrowDomainException()
    {
        // Arrange
        var supplierId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var cost = Money.Create(100, "BRL");
        var costUsd = Money.Create(20, "BRL"); // Wrong currency

        // Act
        Action act = () => Product.Create(supplierId, categoryId, "Test", cost, costUsd);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Custo em USD deve estar na moeda USD");
    }

    [Fact]
    public void Sell_WhenStatusIsCreated_ShouldTransitionToSold()
    {
        // Arrange
        var product = CreateValidProduct();

        // Act
        product.Sell();

        // Assert
        product.Status.Should().Be(ProductStatus.Sold);
        product.SaleDate.Should().NotBeNull();
        product.SaleDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        product.DomainEvents.Should().Contain(e => e is ProductSoldEvent);
    }

    [Fact]
    public void Sell_WhenStatusIsCancelled_ShouldThrowDomainException()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Sell();
        product.Cancel();

        // Act
        Action act = () => product.Sell();

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Produtos cancelados não podem ser vendidos");
    }

    [Fact]
    public void Sell_WhenStatusIsReturned_ShouldThrowDomainException()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Sell();
        product.Return();

        // Act
        Action act = () => product.Sell();

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Produtos devolvidos não podem ser vendidos");
    }

    [Fact]
    public void Sell_WhenAlreadySold_ShouldThrowDomainException()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Sell();

        // Act
        Action act = () => product.Sell();

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Produto já foi vendido");
    }

    [Fact]
    public void Cancel_WhenStatusIsSold_ShouldTransitionToCancelled()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Sell();

        // Act
        product.Cancel();

        // Assert
        product.Status.Should().Be(ProductStatus.Cancelled);
        product.CancellationDate.Should().NotBeNull();
        product.CancellationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        product.DomainEvents.Should().Contain(e => e is ProductCancelledEvent);
    }

    [Fact]
    public void Cancel_WhenStatusIsCreated_ShouldThrowDomainException()
    {
        // Arrange
        var product = CreateValidProduct();

        // Act
        Action act = () => product.Cancel();

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Apenas produtos vendidos podem ser cancelados");
    }

    [Fact]
    public void Return_WhenStatusIsSold_ShouldTransitionToReturned()
    {
        // Arrange
        var product = CreateValidProduct();
        product.Sell();

        // Act
        product.Return();

        // Assert
        product.Status.Should().Be(ProductStatus.Returned);
        product.ReturnDate.Should().NotBeNull();
        product.ReturnDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        product.DomainEvents.Should().Contain(e => e is ProductReturnedEvent);
    }

    [Fact]
    public void Return_WhenStatusIsCreated_ShouldThrowDomainException()
    {
        // Arrange
        var product = CreateValidProduct();

        // Act
        Action act = () => product.Return();

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Apenas produtos vendidos podem ser devolvidos");
    }

    private static Product CreateValidProduct()
    {
        return Product.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Test Product",
            Money.Create(100, "BRL"),
            Money.Create(20, "USD")
        );
    }
}
