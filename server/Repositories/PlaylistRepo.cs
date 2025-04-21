using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.IRepositories;
using server.Shared.DTOs;

namespace server.Repositories;

public class PlaylistRepo : IPlaylistRepo
{
    private readonly AppDbContext _context;
    private readonly ILogger<PlaylistRepo> _logger;

    public PlaylistRepo(AppDbContext context, ILogger<PlaylistRepo> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<PlaylistResponseDto>> GetAllAsync(int userId)
    {
        return _context.Playlists
            .Where(p => p.UserId == userId)
            .AsNoTracking()
            .Select(p => new PlaylistResponseDto(p.Id, p.Name, p.CoverUrl, p.IsSystem))
            .ToListAsync();
    }

    public async Task<PlaylistResponseDto?> GetByIdAsync(int id, int userId)
    {
        var playlist = await _context.Playlists
            .Include(p => p.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        return playlist is null
            ? null
            : new PlaylistResponseDto(playlist.Id, playlist.Name, playlist.CoverUrl, playlist.IsSystem);
    }

    public async Task<PlaylistResponseDto> CreateAsync(CreatePlaylistDto dto, int userId)
    {
        var playlist = new Playlist
        {
            Name = dto.Name,
            CoverUrl = dto.CoverUrl,
            UserId = userId,
            IsSystem = false
        };

        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();

        return new PlaylistResponseDto(
            playlist.Id,
            playlist.Name,
            playlist.CoverUrl,
            playlist.IsSystem);
    }

    public async Task<PlaylistResponseDto> UpdateAsync(int id, int userId, UpdatePlaylistDto dto)
    {
        var existing = await _context.Playlists
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (existing is null)
            throw new KeyNotFoundException("Playlist not found");

        if (existing.IsSystem)
            throw new InvalidOperationException("System playlists cannot be updated.");

        existing.Name = dto.Name;
        existing.CoverUrl = dto.CoverUrl;

        await _context.SaveChangesAsync();

        return new PlaylistResponseDto(existing.Id, existing.Name, existing.CoverUrl, existing.IsSystem);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var playlist = await _context.Playlists
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (playlist is null)
            throw new KeyNotFoundException("Playlist not found");

        if (playlist.IsSystem)
            throw new InvalidOperationException("System playlists cannot be deleted.");

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();
    }
}
