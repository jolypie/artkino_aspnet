namespace server.Shared.DTOs;

public record AuthResponseDto(string AccessToken, UserDetailsResponseDto User);