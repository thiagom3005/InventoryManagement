using InventoryManagement.Domain.Exceptions;

namespace InventoryManagement.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Shortcode { get; private set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }

    // Navigation properties
    public Category? ParentCategory { get; private set; }
    public ICollection<Category> SubCategories { get; private set; } = new List<Category>();

    private Category() { } // EF Core

    public static Category Create(string name, string shortcode, Guid? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome da categoria é obrigatório");

        if (string.IsNullOrWhiteSpace(shortcode))
            throw new DomainException("Shortcode é obrigatório");

        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Shortcode = shortcode.Trim().ToUpper(),
            ParentCategoryId = parentCategoryId,
            SubCategories = new List<Category>()
        };
    }
}
