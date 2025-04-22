using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Playlist
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [MinLength(1)] 
    public string Name { get; set; } = string.Empty;
        
    
    [Url]
    public string? CoverUrl { get; set; } // for user-created playlists
    
    public bool IsSystem { get; set; } = false; // by default (isSystem = true), every user has 3 playlist - favorites, watch later, watched
    
    public List<PlaylistItem> Items { get; set; } = new();
}