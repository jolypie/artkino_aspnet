using server.Shared.DTOs;

namespace server.Services.IServices;

public interface ITmdbService
{
    Task<List<FilmDto>> GetPopularFilmsAsync();
    
}