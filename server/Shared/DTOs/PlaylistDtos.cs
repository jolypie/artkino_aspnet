using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record CreatePlaylistDto
    (
      [property: Required, MinLength(1), MaxLength(100)] string Name,
      [property: Url, MaxLength(100)] string? CoverUrl
    );
    
public record UpdatePlaylistDto
(
    [property: Required, MinLength(1), MaxLength(100)] string Name,
    [property: Url, MaxLength(100)] string? CoverUrl
);

public record PlaylistResponseDto
(
    int Id,
    string Name,
    string? CoverUrl,
    bool IsSystem
);