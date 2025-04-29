using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;

public record GenreDto
(
    int Id,
    [Required, MaxLength(255)] string Name
);


public record GenreResponse
(
    List<GenreDto> Genres
);