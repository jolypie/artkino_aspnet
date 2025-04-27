using server.Shared.DTOs;

namespace server.Repositories.IRepositories;

public interface IReviewRepo
{
    Task<List<ResponseReviewDto>> GetAllByFilmAsync(int tmdbFilmId);
    Task<ResponseReviewDto?> GetByIdAsync(int reviewId);
    Task<ResponseReviewDto> CreateAsync(CreateReviewDto dto, int userId);
    Task<ResponseReviewDto> UpdateAsync(int reviewId, int userId, UpdateReviewDto dto);
    Task DeleteAsync(int reviewId, int userId);
}