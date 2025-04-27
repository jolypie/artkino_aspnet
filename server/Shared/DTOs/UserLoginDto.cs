using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public class UserLoginDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}