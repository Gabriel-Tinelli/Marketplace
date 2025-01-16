namespace Marketplace.CheckoutAPI.DTOs;
public record PaymentDto
{
    public int PaymentId { get; set; }
    
    public int OrderId { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public string PaymentStatus { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    public decimal PaymentAmount { get; set; }
    
}