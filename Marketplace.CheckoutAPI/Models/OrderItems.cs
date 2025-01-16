using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Marketplace.CheckoutAPI.DTOs;

namespace CheckoutAPI.Models

{
    [Table("order_items")]

    public class OrderItems
    {
        public OrderItems(OrderItemsDto dto)
        {
            OrderItemId = dto.OrderItemId;
            ProductId = dto.ProductId;
            Quantity = dto.Quantity;
            Price = dto.Price;
            Total = dto.Total;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }
        
        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }
        
        [ForeignKey("order_id")]
        public virtual Order? Order { get; set; }
        
        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }
        
        [Required]
        [Column("price")]
        public decimal Price { get; set; }
        
        [Required]
        [Column("total")]
        public decimal Total { get; set; }

        public OrderItems()
        {
            
        }
    }
}