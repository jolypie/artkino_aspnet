public record TmdbVideosRaw(List<TmdbVideosRaw.VideoItem> Results)
{
    public record VideoItem(string Key, string Site, string Type);
}
