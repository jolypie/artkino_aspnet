using server.Repositories.IRepositories;
using server.Services.IServices;
using server.Shared.DTOs;

namespace server.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepo _repo;

    public PlaylistService(IPlaylistRepo repo)
    {
        _repo = repo;
    }

    public Task<List<PlaylistResponseDto>> GetAllAsync(int userId) =>
        _repo.GetAllAsync(userId);

    public Task<PlaylistResponseDto?> GetByIdAsync(int id, int userId) =>
        _repo.GetByIdAsync(id, userId);

    public Task<PlaylistResponseDto> CreateAsync(CreatePlaylistDto dto, int userId) =>
        _repo.CreateAsync(dto, userId);

    public Task<PlaylistResponseDto> UpdateAsync(int id, int userId, UpdatePlaylistDto dto) =>
        _repo.UpdateAsync(id, userId, dto);

    public Task DeleteAsync(int id, int userId) =>
        _repo.DeleteAsync(id, userId);
}