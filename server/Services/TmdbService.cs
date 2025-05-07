using System.Text.Json;
using server.External.Tmdb.Models;
using server.Services.IServices;
using server.Shared.DTOs;

namespace server.Services;

public class TmdbService : ITmdbService
{
    private readonly HttpClient              _httpClient;
    private readonly ILogger<TmdbService>    _logger;
    private          Dictionary<int,string>? _genreCache;

    public TmdbService(HttpClient httpClient, ILogger<TmdbService> logger)
    {
        _httpClient = httpClient;
        _logger     = logger;
    }

    private async Task<Dictionary<int,string>> GetGenreDictionaryAsync()
    {
        if (_genreCache is not null) return _genreCache;

        var raw = await _httpClient.GetFromJsonAsync<
                      server.External.Tmdb.Models.GenreListRaw>
                      ("genre/movie/list?language=en-US");

        _genreCache = raw?.Genres.ToDictionary(g => g.Id, g => g.Name) ?? new();
        return _genreCache;
    }

    public async Task<List<GenreDto>> GetGenresAsync()
    {
        var dict = await GetGenreDictionaryAsync();
        return dict.Select(kv => new GenreDto(kv.Key, kv.Value)).ToList();
    }

    public async Task<List<FilmDto>> GetFilmsAsync(string endpoint, int page = 1)
    {
        var raw = await _httpClient.GetFromJsonAsync<
                      server.External.Tmdb.Models.TmdbResultsRaw>
                      ($"{endpoint}?language=en-US&page={page}");

        if (raw?.Results is null || raw.Results.Count == 0)
            return new List<FilmDto>();

        var genres = await GetGenreDictionaryAsync();

        return raw.Results
                  .Where(r => r != null)
                  .Select(r => new FilmDto
                  {
                      TmdbId      = r.Id,
                      Title       = r.Title ?? "Untitled",
                      ReleaseDate = DateTime.TryParse(r.ReleaseDate, out var d) ? d : default,
                      PosterPath  = r.PosterPath ?? "",
                      VoteAverage = (decimal?)r.VoteAverage ?? 0m,
                      Genres      = r.GenreIds is null
                                    ? ""
                                    : string.Join(", ",
                                                  r.GenreIds.Select(id =>
                                                      genres.TryGetValue(id, out var name)
                                                          ? name
                                                          : "Unknown"))
                  })
                  .ToList();
    }

    public async Task<FilmDto> GetFilmDetailsAsync(int id)
    {
        var raw = await _httpClient.GetFromJsonAsync<TmdbFilmDetailsRaw>(
                      $"movie/{id}?language=en-US&append_to_response=credits")
                  ?? throw new Exception("Details not found");

        var director = raw.Credits.Crew.FirstOrDefault(c => c.Job == "Director")?.Name ?? "";
        var cast     = string.Join(", ", raw.Credits.Cast.Take(5).Select(c => c.Name));

        return new FilmDto
        {
            TmdbId      = raw.TmdbId,
            Title       = raw.Title,
            Description = raw.Overview,
            PosterPath  = raw.PosterPath ?? "",
            VoteAverage = (decimal?)raw.VoteAverage ?? 0m,
            ReleaseDate = DateTime.TryParse(raw.ReleaseDate, out var d) ? d : default,
            Genres      = string.Join(", ", raw.Genres.Select(g => g.Name)),
            Countries   = string.Join(", ", raw.Countries.Select(c => c.Name)),
            Director    = director,
            Cast        = cast
        };
    }

    public async Task<string?> GetTrailerKeyAsync(int id)
    {
        var raw = await _httpClient.GetFromJsonAsync<TmdbVideosRaw>(
                      $"movie/{id}/videos?language=en-US");

        return raw?.Results
                   .FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube")
                   ?.Key;
    }
}
