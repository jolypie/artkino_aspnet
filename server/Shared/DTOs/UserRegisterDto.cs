using System.ComponentModel.DataAnnotations;
using server.Shared.Enums;

namespace server.Shared.DTOs;

public class UserRegisterDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
}