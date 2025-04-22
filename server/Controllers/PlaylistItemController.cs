using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;
using server.Shared.DTOs;

[ApiController]
[Route("api/[controller]")]
public class PlaylistItemsController : ControllerBase
{
    private readonly IPlaylistItemService _service;

    public PlaylistItemsController(IPlaylistItemService service)
    {
        _service = service;
    }

    [HttpGet("{playlistId}")]
    public async Task<IActionResult> GetAll(int playlistId)
    {
        var items = await _service.GetAllAsync(playlistId);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistItemDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int playlistId)
    {
        try
        {
            await _service.DeleteAsync(id, playlistId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}