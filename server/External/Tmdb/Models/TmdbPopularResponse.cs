using System.Text.Json.Serialization;
using server.Shared.DTOs;

namespace server.External.Tmdb.Models;

public class TmdbPopularResponse
{
    [JsonPropertyName("results")]
    public List<TmdbFilmRaw> Results { get; set; } = new();
}

public class TmdbFilmRaw
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("vote_average")]
    public decimal VoteAverage { get; set; }
    [JsonPropertyName("film_id")]
    public int Id { get; set; } 
}

