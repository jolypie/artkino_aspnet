using server.Repositories.IRepositories;
using server.Services.IServices;
using server.Shared.DTOs;

namespace server.Services;

public class PlaylistItemService : IPlaylistItemService
{
    private readonly IPlaylistItemRepo _repo;

    public PlaylistItemService(IPlaylistItemRepo repo)
    {
        _repo = repo;
    }

    public Task<List<PlaylistItemResponseDto>> GetAllAsync(int playlistId) =>
        _repo.GetAllAsync(playlistId);

    public Task<PlaylistItemResponseDto> CreateAsync(CreatePlaylistItemDto dto) =>
        _repo.CreateAsync(dto);

    public Task DeleteAsync(int id, int playlistId) =>
        _repo.DeleteAsync(id, playlistId);
}