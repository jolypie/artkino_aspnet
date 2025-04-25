using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Shared.DTOs;

public record CreateReviewDto
(
    [Required] int TmdbFilmId, 
    [Required, MaxLength(2000)] string UserReview
);

public record UpdateReviewDto
(
    [Required] int TmdbFilmId, 
    [Required, MaxLength(2000)] string UserReview
);

public record ResponseReviewDto
(
    int UserId,
    int TmdbFilmId, 
    string UserReview,
    DateTime CreatedAt
);
