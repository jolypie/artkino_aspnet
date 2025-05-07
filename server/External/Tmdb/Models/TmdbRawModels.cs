using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace server.External.Tmdb.Models;

public record TmdbResultsRaw(
    [property: JsonPropertyName("results")]
    List<TmdbFilmShortRaw>? Results);

public record TmdbFilmShortRaw(
    [property: JsonPropertyName("id")]           int            Id,
    [property: JsonPropertyName("title")]        string?        Title,
    [property: JsonPropertyName("release_date")] string?        ReleaseDate,
    [property: JsonPropertyName("poster_path")]  string?        PosterPath,
    [property: JsonPropertyName("vote_average")] double?        VoteAverage,
    [property: JsonPropertyName("genre_ids")]    List<int>?     GenreIds);

public class TmdbFilmDetailsRaw
{
    [JsonPropertyName("id")]            public int    TmdbId      { get; set; }
    [JsonPropertyName("title")]         public string Title       { get; set; } = "";
    [JsonPropertyName("overview")]      public string Overview    { get; set; } = "";
    [JsonPropertyName("poster_path")]   public string? PosterPath { get; set; }
    [JsonPropertyName("release_date")]  public string? ReleaseDate{ get; set; }
    [JsonPropertyName("vote_average")]  public double? VoteAverage{ get; set; }

    [JsonPropertyName("genres")]               public List<GenreRaw>   Genres    { get; set; } = new();
    [JsonPropertyName("production_countries")] public List<CountryRaw> Countries { get; set; } = new();
    [JsonPropertyName("credits")]              public CreditsRaw       Credits   { get; set; } = new();
}

public record GenreRaw   ([property: JsonPropertyName("id")] int Id,
                          [property: JsonPropertyName("name")] string Name);

public record CountryRaw([property: JsonPropertyName("name")] string Name);

public class CreditsRaw
{
    [JsonPropertyName("cast")] public List<CastRaw> Cast { get; set; } = new();
    [JsonPropertyName("crew")] public List<CrewRaw> Crew { get; set; } = new();
}
public record CastRaw ([property: JsonPropertyName("name")] string Name);
public record CrewRaw ([property: JsonPropertyName("job")]  string Job,
                       [property: JsonPropertyName("name")] string Name);

public record TmdbVideosRaw(
    [property: JsonPropertyName("results")] List<TmdbVideosRaw.VideoItem> Results)
{
    public record VideoItem(
        [property: JsonPropertyName("key")]  string Key,
        [property: JsonPropertyName("site")] string Site,
        [property: JsonPropertyName("type")] string Type);
}

public record GenreListRaw(
    [property: JsonPropertyName("genres")] List<GenreRaw> Genres);
