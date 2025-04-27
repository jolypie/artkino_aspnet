using server.Repositories.IRepositories;
using server.Services.IServices;
using server.Shared.DTOs;

namespace server.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepo _repo;

    public ReviewService(IReviewRepo repo)
    {
        _repo = repo;
    }

    public Task<List<ResponseReviewDto>> GetAllByFilmAsync(int tmdbFilmId) =>
        _repo.GetAllByFilmAsync(tmdbFilmId);

    public Task<ResponseReviewDto?> GetByIdAsync(int reviewId) =>
        _repo.GetByIdAsync(reviewId);

    public Task<ResponseReviewDto> CreateAsync(CreateReviewDto dto, int userId) =>
        _repo.CreateAsync(dto, userId);

    public Task<ResponseReviewDto> UpdateAsync(int reviewId, int userId, UpdateReviewDto dto) =>
        _repo.UpdateAsync(reviewId, userId, dto);

    public Task DeleteAsync(int reviewId, int userId) =>
        _repo.DeleteAsync(reviewId, userId);
}