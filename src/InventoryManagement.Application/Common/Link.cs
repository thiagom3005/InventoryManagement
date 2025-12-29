namespace InventoryManagement.Application.Common;

public record Link(string Href, string Rel, string Method = "GET")
{
    public static Link Create(string href, string rel, string method = "GET")
        => new(href, rel, method);
}
