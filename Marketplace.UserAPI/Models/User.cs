using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.UserAPI.Models;

public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; }

    //[Required]
    //[Column("password_hash")]
    //public string PasswordHash { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("role")]
    public string? Role { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}