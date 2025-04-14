using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required] 
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    [Required]
    public int TmdbFilmId { get; set; }
    [Column(TypeName = "varchar(2000)")]
    public string UserReview { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}