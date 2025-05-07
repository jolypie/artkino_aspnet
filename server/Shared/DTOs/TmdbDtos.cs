namespace server.Shared.DTOs;

public record TmdbResultsRaw(
    List<TmdbFilmShortRaw> Results
);

public record TmdbFilmShortRaw(
    int Id,
    string Title,
    string? PosterPath,
    string? ReleaseDate,
    double VoteAverage,
    List<int> GenreIds
);
