using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryService.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        public int CategoryID { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Column("category_name")]
        public string CategoryName { get; set; }

    }
}