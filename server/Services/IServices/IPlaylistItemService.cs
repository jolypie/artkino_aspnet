using server.Shared.DTOs;

namespace server.Services.IServices;

public interface IPlaylistItemService
{
    Task<List<PlaylistItemResponseDto>> GetAllAsync(int playlistId);
    Task<PlaylistItemResponseDto> CreateAsync(CreatePlaylistItemDto dto);
    Task DeleteAsync(int id, int playlistId);
}