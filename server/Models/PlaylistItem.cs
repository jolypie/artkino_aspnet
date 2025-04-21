using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class PlaylistItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;

    [Required]
    public int TmdbFilmId { get; set; }

    [Required]
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}