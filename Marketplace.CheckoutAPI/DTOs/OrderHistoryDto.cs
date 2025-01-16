namespace Marketplace.CheckoutAPI.DTOs;
public record OrderHistoryDto
{
    public int StatusHistoryId { get; set; }
    
    public int OrderId { get; set; }
    
    public string Status { get; set; }
    
    public DateTime ChangeData { get; set; }
}