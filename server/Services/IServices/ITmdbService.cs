using server.Shared.DTOs;

namespace server.Services.IServices;

public interface ITmdbService
{
    Task<List<FilmDto>> GetPopularFilmsAsync();
    Task<FilmDto> GetFilmDetailsAsync(int id);
    Task<List<GenreDto>> GetGenresAsync();
}