using System.Text.Json;
using server.External.Tmdb.Models;
using server.Services.IServices;
using server.Shared.DTOs;

namespace server.Services;

public class TmdbService : ITmdbService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public TmdbService(HttpClient httpClient, ILogger<TmdbService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<List<FilmDto>> GetPopularFilmsAsync()
    {
        var allPopularFilms = new List<FilmDto>();
        var pageNum = 2;
        for (var page = 1; page <= pageNum; page++)
        {
            var response =
                await _httpClient.GetAsync(
                    $"movie/popular?language=en-US&page={page}&region=US");
            response.EnsureSuccessStatusCode();
            
            using var stream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<TmdbRawFilmResponse>(
                stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if (data is null) break;
            
            allPopularFilms.AddRange(
                data.Results.Select(raw => new FilmDto
                {
                    TmdbId = raw.TmdbId,
                    Title = raw.Title,
                    Description = raw.Overview,
                    PosterPath  = raw.PosterPath,
                    VoteAverage = raw.VoteAverage,
                    ReleaseDate = DateTime.TryParse(raw.ReleaseDate, out var d) ? d : default
                }));
        }
        
        return allPopularFilms;
    }

    public async Task<FilmDto> GetFilmDetailsAsync(int id)
    {
        var film = new FilmDto();
        var response = await _httpClient.GetAsync($"movie/{id}?language=en-US");
        response.EnsureSuccessStatusCode();
        
    }
}