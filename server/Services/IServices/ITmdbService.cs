using server.Shared.DTOs;
using Raw = server.External.Tmdb.Models;

namespace server.Services.IServices;

public interface ITmdbService
{
    Task<List<FilmDto>> GetFilmsAsync(string endpoint, int page = 1);
    Task<FilmDto>       GetFilmDetailsAsync(int id);
    Task<string?>       GetTrailerKeyAsync(int id);
    Task<List<GenreDto>> GetGenresAsync();
    Task<List<FilmDto>> SearchFilmsAsync(string query, int page = 1);
    Task<List<FilmDto>> MapRawToDtoAsync(IEnumerable<Raw.TmdbFilmShortRaw>? raws);
}