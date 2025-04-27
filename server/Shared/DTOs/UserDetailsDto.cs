using System.ComponentModel.DataAnnotations;
using server.Shared.Enums;

namespace server.Shared.DTOs;

public record UserDetailsResponseDto([Required] string Username, Role Role);