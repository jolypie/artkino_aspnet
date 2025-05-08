using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.IRepositories;
using server.Shared.DTOs;

namespace server.Repositories;

public class PlaylistItemRepo : IPlaylistItemRepo
{
    private readonly AppDbContext _context;
    private readonly ILogger<PlaylistRepo> _logger;

    public PlaylistItemRepo(AppDbContext context, ILogger<PlaylistRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    
    public Task<List<PlaylistItemResponseDto>> GetAllAsync(int playlistId)
    {
        return _context.PlaylistItems
            .Where(p => p.PlaylistId == playlistId)
            .AsNoTracking()
            .Select(p => new PlaylistItemResponseDto(p.Id, p.PlaylistId, p.TmdbFilmId, p.AddedAt))
            .ToListAsync();
    }

    public async Task<PlaylistItemResponseDto> CreateAsync(CreatePlaylistItemDto dto)
    {
        var exists = await _context.PlaylistItems
            .AnyAsync(p => p.PlaylistId == dto.PlaylistId && p.TmdbFilmId == dto.TmdbFilmId);

        if (exists)
            throw new Exception("Film already in playlist");

        var playlistItem = new PlaylistItem
        {
            PlaylistId = dto.PlaylistId,
            TmdbFilmId = dto.TmdbFilmId
        };

        await _context.PlaylistItems.AddAsync(playlistItem);
        await _context.SaveChangesAsync();

        return new PlaylistItemResponseDto(
            playlistItem.Id,
            playlistItem.PlaylistId,
            playlistItem.TmdbFilmId,
            playlistItem.AddedAt);
    }


    public async Task DeleteAsync(int id, int playlistId)
    {
        var playlistItem = await _context.PlaylistItems
            .FirstOrDefaultAsync(p => p.Id == id && p.PlaylistId == playlistId);
        
        if(playlistItem is null)
            throw new Exception("Playlist item not found");
        
        _context.PlaylistItems.Remove(playlistItem);
        await _context.SaveChangesAsync();
    }
}