using server.Shared.DTOs;

namespace server.Services.IServices;

public interface IPlaylistService
{
    Task<List<PlaylistResponseDto>> GetAllAsync(int userId);
    Task<PlaylistResponseDto?> GetByIdAsync(int id, int userId);
    Task<PlaylistResponseDto> CreateAsync(CreatePlaylistDto dto, int userId);
    Task<PlaylistResponseDto> UpdateAsync(int id, int userId, UpdatePlaylistDto dto);
    Task DeleteAsync(int id, int userId);
}