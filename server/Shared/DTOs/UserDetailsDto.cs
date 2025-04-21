using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record UserDetailsResponseDto
(
    [property: Required] string Username
);