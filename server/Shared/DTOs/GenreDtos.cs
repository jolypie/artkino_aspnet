using System.ComponentModel.DataAnnotations;

namespace server.Shared.DTOs;


public record GenreDto(int Id, [Required, MaxLength(255)] string Name);

public record GenreResponse(List<GenreRaw> Genres);

public record GenreRaw(int Id, string Name);

public record GenreListRaw(List<GenreRaw> Genres);
