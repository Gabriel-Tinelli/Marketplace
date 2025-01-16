using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Marketplace.CheckoutAPI.DTOs;

namespace CheckoutAPI.Models
{
    [Table("order_status_history")]
    public class OrderHistory
    {
        public OrderHistory(OrderHistoryDto dto)
        {
            StatusHistoryId = dto.StatusHistoryId;
            OrderId = dto.OrderId;
            Status = dto.Status;
            ChangeData = dto.ChangeData;
        }
        
        public OrderHistory() { }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("status_history_id")]
        public int StatusHistoryId { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        [Column("change_data")]
        public DateTime ChangeData { get; set; }
    }
}