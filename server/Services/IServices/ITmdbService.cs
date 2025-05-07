using server.Shared.DTOs;

namespace server.Services.IServices;

public interface ITmdbService
{
    Task<List<FilmDto>> GetFilmsAsync(string endpoint, int page = 1);
    Task<FilmDto>       GetFilmDetailsAsync(int id);
    Task<string?>       GetTrailerKeyAsync(int id);
    Task<List<GenreDto>> GetGenresAsync();
}