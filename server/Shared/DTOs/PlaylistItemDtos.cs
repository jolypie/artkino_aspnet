using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record CreatePlaylistItemDto
(
    [Required] int PlaylistId,
    [Required] int TmdbFilmId
);

public record PlaylistItemResponseDto
(
    int Id,
    int PlaylistId,
    int TmdbFilmId,
    DateTime AddedAt
);