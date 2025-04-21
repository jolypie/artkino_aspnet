using server.Shared.DTOs;

namespace server.Repositories.IRepositories;

public interface IPlaylistItemRepo
{
    Task<List<PlaylistItemResponseDto>> GetAllAsync(int playlistId);
    Task<PlaylistItemResponseDto> CreateAsync(CreatePlaylistItemDto dto);
    Task DeleteAsync(int id, int playlistId);
}