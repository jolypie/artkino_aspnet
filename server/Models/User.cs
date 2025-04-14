using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Username { get; set; } = string.Empty;

    [Required]
    public byte[] PasswordHash { get; set; } = null!;

    [Required]
    public byte[] PasswordSalt { get; set; } = null!;
}
