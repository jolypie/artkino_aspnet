using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record UserDetailsResponseDto
(
    [Required] string Username
);