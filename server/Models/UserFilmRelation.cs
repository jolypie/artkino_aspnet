namespace server.Models;

public class UserFilmRelation
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int FilmId { get; set; }
    public Film Film { get; set; } = null!;

    [Column(TypeName = "varchar(20)")]
    [EnumDataType(typeof(FilmRelationType))]
    public FilmRelationType Type { get; set; }
}
