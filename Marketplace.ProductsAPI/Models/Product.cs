using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsService.Models

{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int CategoryID { get; set; }
        
        [Required]
        [MaxLength(255)]
        [Column("product_name")]
        public string ProductName { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public int Stock { get; set; }
        
        [MaxLength]
        [Column("image_url")]
        public string? ImageURL { get; set; }
        
        public DateTime? DateCreated { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}