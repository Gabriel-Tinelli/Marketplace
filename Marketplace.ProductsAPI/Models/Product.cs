using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsService.Models

{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductID { get; set; }
        
        [Required]
        [Column("user_id")]
        public int UserID { get; set; }
        
        [Required]
        [Column("category_id")]
        public int CategoryID { get; set; }
        
        [Required]
        [MaxLength(255)]
        [Column("product_name")]
        public string ProductName { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("price")]
        public decimal Price { get; set; }
        
        [Required]
        [Column("stock")]
        public int Stock { get; set; }
        
        [MaxLength]
        [Column("image_url")]
        public string? ImageURL { get; set; }
        
        
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}