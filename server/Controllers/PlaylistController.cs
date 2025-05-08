using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;
using server.Shared.DTOs;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _service;

    public PlaylistsController(IPlaylistService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new UnauthorizedAccessException("User ID claim not found");
        return int.Parse(claim.Value);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var playlists = await _service.GetAllAsync(userId);
        return Ok(playlists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var playlist = await _service.GetByIdAsync(id, userId);
        if (playlist == null)
            return NotFound("Playlist not found");

        return Ok(playlist);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistDto dto)
    {
        try
        {
            var userId = GetUserId();
            var created = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlaylistDto dto)
    {
        try
        {
            var userId = GetUserId();
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = GetUserId();
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
