using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TmdbController : ControllerBase
{
    private readonly ITmdbService _tmdb;
    public TmdbController(ITmdbService tmdb) => _tmdb = tmdb;

    [HttpGet("popular"  )] public async Task<IActionResult> Popular  ([FromQuery]int page=1) =>
        Ok(await _tmdb.GetFilmsAsync("movie/popular",        page));

    [HttpGet("trending" )] public async Task<IActionResult> Trending ([FromQuery]int page=1) =>
        Ok(await _tmdb.GetFilmsAsync("trending/movie/day",   page));

    [HttpGet("top250"   )] public async Task<IActionResult> Top250   () =>
        Ok((await _tmdb.GetFilmsAsync("movie/top_rated")).Take(250));

    [HttpGet("genre/{id:int}")]
    public async Task<IActionResult> ByGenre(int id, [FromQuery]int page=1) =>
        Ok(await _tmdb.GetFilmsAsync($"discover/movie?with_genres={id}", page));

    [HttpGet("movie_details/{id:int}")] public async Task<IActionResult> Details(int id) =>
        Ok(await _tmdb.GetFilmDetailsAsync(id));

    [HttpGet("trailer/{id:int}")]       public async Task<IActionResult> Trailer(int id) =>
        Ok(new { key = await _tmdb.GetTrailerKeyAsync(id) });

    [HttpGet("genres")]                 public async Task<IActionResult> Genres() =>
        Ok(new { genres = await _tmdb.GetGenresAsync() });
}