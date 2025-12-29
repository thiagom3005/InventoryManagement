using FluentAssertions;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.Domain.ValueObjects;

namespace InventoryManagement.UnitTests.Domain;

public class EmailTests
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("a@b.c")]
    public void Create_WithValidEmail_ShouldCreateEmail(string validEmail)
    {
        // Act
        var email = Email.Create(validEmail);

        // Assert
        email.Value.Should().Be(validEmail.ToLowerInvariant());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid")]
    [InlineData("@test")]
    [InlineData("test@")]
    [InlineData("ab")]
    public void Create_WithInvalidEmail_ShouldThrowDomainException(string invalidEmail)
    {
        // Act
        Action act = () => Email.Create(invalidEmail);

        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Email inválido");
    }

    [Fact]
    public void Create_ShouldNormalizeToLowerCase()
    {
        // Act
        var email = Email.Create("Test@EXAMPLE.COM");

        // Assert
        email.Value.Should().Be("test@example.com");
    }
}
