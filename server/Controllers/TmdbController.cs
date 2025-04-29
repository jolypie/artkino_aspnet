using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class TmdbController : ControllerBase
{
    private readonly ITmdbService _tmdb;

    public TmdbController(ITmdbService tmdb)
    {
        _tmdb = tmdb;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopular()
    {
        var films = await _tmdb.GetPopularFilmsAsync();
        return Ok(films);
    }

    [HttpGet("movie_details/{id}")]
    public async Task<IActionResult> GetMovieDetails(int id)
    {
        var film = await _tmdb.GetFilmDetailsAsync(id);
        return Ok(film);
    }
    
    [HttpGet("genres")]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _tmdb.GetGenresAsync();
        return Ok(new { genres });
    }

}