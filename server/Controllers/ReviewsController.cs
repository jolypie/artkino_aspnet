using Microsoft.AspNetCore.Mvc;
using server.Services.IServices;
using server.Shared.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    [HttpGet("film/{tmdbFilmId}")]
    public async Task<IActionResult> GetAllByFilm(int tmdbFilmId)
    {
        var reviews = await _service.GetAllByFilmAsync(tmdbFilmId);
        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetById(int reviewId)
    {
        var review = await _service.GetByIdAsync(reviewId);
        if (review is null)
            return NotFound("Review not found");
        
        return Ok(review);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReviewDto dto, [FromQuery] int userId)
    {
        var created = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { reviewId = created.Id }, created);
    }

    [HttpPut("{reviewId}")]
    public async Task<IActionResult> Update(int reviewId, [FromBody] UpdateReviewDto dto, [FromQuery] int userId)
    {
        try
        {
            var updated = await _service.UpdateAsync(reviewId, userId, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Review not found");
        }
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> Delete(int reviewId, [FromQuery] int userId)
    {
        try
        {
            await _service.DeleteAsync(reviewId, userId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Review not found");
        }
    }
}