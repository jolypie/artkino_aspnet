using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Shared.DTOs;

public class FilmDto
{
    [Required]
    public int TmdbId { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; } = default;

    [MaxLength(500)]
    public string PosterPath { get; set; } = string.Empty;

    [Column(TypeName = "decimal(3,1)")]
    public decimal VoteAverage { get; set; } = 0;

    [MaxLength(255)]
    public string Countries { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Genres { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Director { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Cast { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
}