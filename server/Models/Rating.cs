using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int TmdbFilmId { get; set; }

    [Required]
    [Range(1, 10)]
    public int Value { get; set; }

    public DateTime RatedAt { get; set; } = DateTime.UtcNow;
}