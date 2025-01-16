using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Marketplace.CheckoutAPI.DTOs;

namespace CheckoutAPI.Models
{
    [Table("payments")]
    public class Payment
    {
        public Payment(PaymentDto dto)
        {
            PaymentId = dto.PaymentId;
            OrderId = dto.OrderId;
            PaymentMethod = dto.PaymentMethod;
            PaymentStatus = dto.PaymentStatus;
            PaymentDate = dto.PaymentDate;
            PaymentAmount = dto.PaymentAmount;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("payment_id")]
        public int PaymentId { get; set; }
        
        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }
        
        [ForeignKey("order_id")]
        public virtual Order? Order { get; set; }
        
        [Required]
        [Column("payment_method")]
        public string PaymentMethod { get; set; }
        
        [Required]
        [Column("payment_status")]
        public string PaymentStatus { get; set; }
        
        [Required]
        [Column("payment_date")]
        public DateTime PaymentDate { get; set; }
        
        [Required]
        [Column("payment_amount")]
        public decimal PaymentAmount { get; set; }

        public Payment() { }
    }
}