using System.Text.Json.Serialization;
using server.Shared.DTOs;

namespace server.External.Tmdb.Models;

public class TmdbPopularFilmsResponse
{
    [JsonPropertyName("results")]
    public List<TmdbPopularFilmRaw> Results { get; set; } = new();
}

public class TmdbFilmBaseRaw
{
    [JsonPropertyName("id")]
    public int TmdbId { get; set; }
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
}

public class TmdbPopularFilmRaw : TmdbFilmBaseRaw
{
}

public class TmdbFilmDetailsRaw : TmdbFilmBaseRaw
{
    [JsonPropertyName("genres")]
    public List<Genre> Genres { get; set; }
    [JsonPropertyName("production_countries")]
    public List<Country> Countries { get; set; }
    [JsonPropertyName("credits")]
    public Credits Credits { get; set; }
}

public class Genre
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Country
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Credits
{
    [JsonPropertyName("cast")]
    public List<CastMember> Cast { get; set; }

    [JsonPropertyName("crew")]
    public List<CrewMember> Crew { get; set; }
}

public class CastMember
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class CrewMember
{
    [JsonPropertyName("job")]
    public string Job { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
