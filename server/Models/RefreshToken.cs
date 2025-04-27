using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string TokenHash { get; set; } = string.Empty;

    [Required]
    public DateTime Expires { get; set; }

    public bool Revoked { get; set; } = false;

    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}