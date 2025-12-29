using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Common;

public static class HateoasLinkGenerator
{
    public static List<Link> GenerateProductLinks(Guid productId, ProductStatus status, string baseUrl = "/api/products")
    {
        var links = new List<Link>
        {
            Link.Create($"{baseUrl}/{productId}", "self", "GET"),
            Link.Create($"{baseUrl}/{productId}/supplier", "supplier", "GET"),
            Link.Create($"{baseUrl}/{productId}/category", "category", "GET")
        };

        // Links dinâmicos baseados no estado do produto
        switch (status)
        {
            case ProductStatus.Created:
                links.Add(Link.Create($"{baseUrl}/{productId}/sales", "sell", "POST"));
                break;

            case ProductStatus.Sold:
                links.Add(Link.Create($"{baseUrl}/{productId}/cancellations", "cancel", "POST"));
                links.Add(Link.Create($"{baseUrl}/{productId}/returns", "return", "POST"));
                break;

            case ProductStatus.Cancelled:
            case ProductStatus.Returned:
                // Estados finais - sem ações disponíveis
                break;
        }

        return links;
    }

    public static List<Link> GenerateCategoryLinks(Guid categoryId, string baseUrl = "/api/categories")
    {
        return new List<Link>
        {
            Link.Create($"{baseUrl}/{categoryId}", "self", "GET"),
            Link.Create($"{baseUrl}/{categoryId}/products", "products", "GET"),
            Link.Create($"{baseUrl}/{categoryId}", "delete", "DELETE")
        };
    }

    public static List<Link> GenerateSupplierLinks(Guid supplierId, string baseUrl = "/api/suppliers")
    {
        return new List<Link>
        {
            Link.Create($"{baseUrl}/{supplierId}", "self", "GET"),
            Link.Create($"{baseUrl}/{supplierId}/products", "products", "GET"),
            Link.Create($"{baseUrl}/{supplierId}", "delete", "DELETE")
        };
    }
}
