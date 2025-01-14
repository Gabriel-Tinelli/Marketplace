namespace Marketplace.ProductsAPI.DTOs;

public record ProductDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? ImageURL { get; set; }

    public int CategoryId { get; set; }
}