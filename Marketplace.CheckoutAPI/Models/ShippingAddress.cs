using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Marketplace.CheckoutAPI.DTOs;

namespace CheckoutAPI.Models
{
    [Table("shipping_address")]
    public class ShippingAddress
    {
        public ShippingAddress(ShippingAddressDto dto)
        {
            AddressID = dto.AddressId;
            OrderId = dto.OrderId;
            Street = dto.Street;
            Number = dto.Number;
            City = dto.City;
            State = dto.State;
            PostalCode = dto.PostalCode;
            Country = dto.Country;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("address_id")]
        public int AddressID { get; set; }
        
        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }
        
        [ForeignKey("order_id")]
        public virtual Order? Order { get; set; }
        
        [Column("street")]
        public string Street { get; set; }
        
        [Column("number")]
        public string Number { get; set; }
        
        [Column("city")]
        public string City { get; set; }
        
        [Column("state")]
        public string State { get; set; }
        
        [Column("postal_code")]
        public string PostalCode { get; set; }
        
        [Column("country")]
        public string Country { get; set; }

        public ShippingAddress() { }
    }
    
}