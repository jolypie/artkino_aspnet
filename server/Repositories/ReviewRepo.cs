using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.IRepositories;
using server.Shared.DTOs;

namespace server.Repositories;

public class ReviewRepo : IReviewRepo
{
    private readonly AppDbContext _context;
    private readonly ILogger<ReviewRepo> _logger;

    public ReviewRepo(AppDbContext context, ILogger<ReviewRepo> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ResponseReviewDto>> GetAllByFilmAsync(int tmdbFilmId)
    {
        return await _context.Reviews
            .Where(r => r.TmdbFilmId == tmdbFilmId)
            .AsNoTracking()
            .Select(r => new ResponseReviewDto(r.Id, r.UserId, r.TmdbFilmId, r.UserReview, r.CreatedAt))
            .ToListAsync();
    }

    public async Task<ResponseReviewDto?> GetByIdAsync(int reviewId)
    {
        var review = await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reviewId);

        return review is null
            ? null
            : new ResponseReviewDto(review.Id, review.UserId, review.TmdbFilmId, review.UserReview, review.CreatedAt);
    }

    public async Task<ResponseReviewDto> CreateAsync(CreateReviewDto dto, int userId)
    {
        var review = new Review
        {
            UserId = userId,
            TmdbFilmId = dto.TmdbFilmId,
            UserReview = dto.UserReview
        };

        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return new ResponseReviewDto(review.Id, review.UserId, review.TmdbFilmId, review.UserReview, review.CreatedAt);
    }

    public async Task<ResponseReviewDto> UpdateAsync(int reviewId, int userId, UpdateReviewDto dto)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review is null)
            throw new KeyNotFoundException("Review not found");

        review.TmdbFilmId = dto.TmdbFilmId;
        review.UserReview = dto.UserReview;

        await _context.SaveChangesAsync();

        return new ResponseReviewDto(review.Id, review.UserId, review.TmdbFilmId, review.UserReview, review.CreatedAt);
    }

    public async Task DeleteAsync(int reviewId, int userId)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);

        if (review is null)
            throw new KeyNotFoundException("Review not found");

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
}
