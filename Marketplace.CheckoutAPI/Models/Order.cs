using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Marketplace.CheckoutAPI.DTOs;

namespace CheckoutAPI.Models
{
    [Table("orders")]
    public class Order
    {
        public Order(OrderDto dto)
        {
            OrderId = dto.OrderId;
            OrderDate = dto.OrderDate;
            TotalAmount = dto.TotalAmount;
            Status = dto.Status;
        }

        public Order() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }
        
        [Required]
        [Column("order_date")]
        public DateTime OrderDate { get; set; }
        
        [Required]
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
    }
}