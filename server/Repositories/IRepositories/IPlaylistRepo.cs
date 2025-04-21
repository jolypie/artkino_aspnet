using server.Models;
using server.Shared.DTOs;

namespace server.Repositories.IRepositories;

public interface IPlaylistRepo
{
    Task<List<PlaylistResponseDto>> GetAllAsync(int userId);
    Task<PlaylistResponseDto?> GetByIdAsync(int id, int userId);
    Task<PlaylistResponseDto> CreateAsync(CreatePlaylistDto dto, int userId);
    Task<PlaylistResponseDto> UpdateAsync(int id, int userId, UpdatePlaylistDto dto);
    Task DeleteAsync(int id, int userId);
}
