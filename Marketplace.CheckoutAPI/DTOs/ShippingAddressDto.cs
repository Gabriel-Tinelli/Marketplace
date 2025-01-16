namespace Marketplace.CheckoutAPI.DTOs;
public record ShippingAddressDto
{
    public int AddressId { get; set; }
    
    public int OrderId { get; set; }
    
    public string Street { get; set; }
    
    public string Number { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string PostalCode { get; set; }
    
    public string Country { get; set; }
}