using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record CreatePlaylistDto
    (
      [Required, MinLength(1), MaxLength(100)] string Name,
      [Url] string? CoverUrl    
    );
    
public record UpdatePlaylistDto
(
    [Required, MinLength(1), MaxLength(100)] string Name,
    [Url] string? CoverUrl
);

public record PlaylistResponseDto
(
    int Id,
    string Name,
    string? CoverUrl,
    bool IsSystem
);

