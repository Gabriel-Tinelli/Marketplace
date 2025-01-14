using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CategoryService.Models;
using Marketplace.ProductsAPI.DTOs;

namespace ProductsService.Models
{
    [Table("products")]
    public class Product
    {
        public Product(ProductDto dto)
        {
            ProductName = dto.ProductName;
            ProductId = dto.ProductId;
            Description = dto.Description;
            Price = dto.Price;
            ImageURL = dto.ImageURL;
            Stock = dto.Stock;
            CategoryId = dto.CategoryId;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Required]
        [MaxLength(255)]
        [Column("product_name")]
        public string ProductName { get; set; }
        
        [Column("description")]
        public string? Description { get; set; }
        
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
        
        // Foreing Key com category
        
        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        
        // Relacionamento simulado com user_id
        
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        public Product()
        {
                
        }
    }
}