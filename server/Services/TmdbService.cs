using System.Text.Json;
using server.Services.IServices;
using server.Shared.DTOs;
using Raw = server.External.Tmdb.Models;

namespace server.Services;

public class TmdbService : ITmdbService
{
    private readonly HttpClient           _httpClient;
    private readonly ILogger<TmdbService> _logger;
    private          Dictionary<int,string>? _genreCache;

    public TmdbService(HttpClient httpClient, ILogger<TmdbService> logger)
    {
        _httpClient = httpClient;
        _logger     = logger;
    }


    private async Task<Dictionary<int,string>> GetGenreDictionaryAsync()
    {
        if (_genreCache is not null) return _genreCache;

        var raw = await _httpClient.GetFromJsonAsync<Raw.GenreListRaw>(
                      "genre/movie/list?language=en-US");

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
        var raw = await _httpClient.GetFromJsonAsync<Raw.TmdbResultsRaw>(
                      $"{endpoint}?language=en-US&page={page}");

        if (raw?.Results is null || raw.Results.Count == 0)
            return new List<FilmDto>();

        return await MapRawToDtoAsync(raw.Results);
    }

    public async Task<FilmDto> GetFilmDetailsAsync(int id)
    {
        var raw = await _httpClient.GetFromJsonAsync<Raw.TmdbFilmDetailsRaw>(
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
            Genres      = raw.Genres
                            .Select(g => new GenreDto(g.Id, g.Name))
                            .ToList(),
            Countries   = string.Join(", ", raw.Countries.Select(c => c.Name)),
            Director    = director,
            Cast        = cast
        };
    }


    public async Task<string?> GetTrailerKeyAsync(int id)
    {
        var raw = await _httpClient.GetFromJsonAsync<Raw.TmdbVideosRaw>(
                      $"movie/{id}/videos?language=en-US");

        return raw?.Results
                   .FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube")
                   ?.Key;
    }


    public async Task<List<FilmDto>> SearchFilmsAsync(string query, int page = 1)
    {
        var encoded = Uri.EscapeDataString(query);

        var raw = await _httpClient.GetFromJsonAsync<Raw.TmdbResultsRaw>(
            $"search/movie?query={encoded}&language=en-US&page={page}&include_adult=false");

        var ordered = raw?.Results?
            .OrderByDescending(r => r.VoteCount);

        return await MapRawToDtoAsync(ordered);
    }

    public async Task<List<FilmDto>> MapRawToDtoAsync(IEnumerable<Raw.TmdbFilmShortRaw>? raws)
    {
        var genreDict = await GetGenreDictionaryAsync();

        return raws?
            .Select(r => new FilmDto
            {
                TmdbId      = r.Id,
                Title       = r.Title ?? "Untitled",
                PosterPath  = r.PosterPath ?? "",
                ReleaseDate = DateTime.TryParse(r.ReleaseDate, out var d) ? d : default,
                VoteAverage = (decimal?)r.VoteAverage ?? 0m,
                Genres      = r.GenreIds?
                    .Where(id => genreDict.ContainsKey(id))
                    .Select(id => new GenreDto(id, genreDict[id]))
                    .ToList()
                    ?? new List<GenreDto>(),
                Countries = "", 
                Description = "", 
                Director = "",
                Cast = ""
            })
            .ToList()
            ?? new();
    }

    
}
