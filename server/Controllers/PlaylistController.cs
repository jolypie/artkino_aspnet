using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;
using server.Shared.DTOs;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _service;

    public PlaylistsController(IPlaylistService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int userId)
    {
        var playlists = await _service.GetAllAsync(userId);
        return Ok(playlists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] int userId)
    {
        var playlist = await _service.GetByIdAsync(id, userId);
        if (playlist == null)
            return NotFound("Playlist not found");

        return Ok(playlist);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistDto dto, [FromQuery] int userId)
    {
        try
        {
            var created = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, userId }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromQuery] int userId, [FromBody] UpdatePlaylistDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, userId, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Playlist not found");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
    {
        try
        {
            await _service.DeleteAsync(id, userId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Playlist not found");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
