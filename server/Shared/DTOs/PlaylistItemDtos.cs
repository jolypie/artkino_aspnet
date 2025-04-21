using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record CreatePlaylistItemDto
(
    [property: Required] int PlaylistId,
    [property: Required] int TmdbFilmId
);

public record PlaylistItemResponseDto
(
    int Id,
    int PlaylistId,
    int TmdbFilmId,
    DateTime AddedAt
);