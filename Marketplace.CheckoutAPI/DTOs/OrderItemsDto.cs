namespace Marketplace.CheckoutAPI.DTOs;
public record OrderItemsDto

{
    public int OrderItemId { get; set; }
    
    public int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Total { get; set; }
    
    
}