using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class Film
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "date")]
    public DateTime RealeaseDate { get; set; } = default;

    [Column(TypeName = "varchar(500)")]
    public string PosterPath { get; set; } = string.Empty;

    [Column(TypeName = "decimal(3,1)")]
    public decimal VoteAverage { get; set; } = default;

    [Column(TypeName = "varchar(255)")]
    public string Countries { get; set; } = string.Empty;

    [Column(TypeName = "varchar(255)")]
    public string Genres { get; set; } = string.Empty;

    [Column(TypeName = "varchar(255)")]
    public string Director { get; set; } = string.Empty;

    [Column(TypeName = "varchar(500)")]
    public string Cast { get; set; } = string.Empty;

    [Column(TypeName = "varchar(2000)")]
    public string Description { get; set; } = string.Empty;
}