using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public AppDbContext() { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistItem> PlaylistItems => Set<PlaylistItem>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnConfiguring(DbContextOptionsBuilder b)
    {
        if (b.IsConfigured) return;

        var conn = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                   ?? throw new("CONNECTION_STRING env var missing for CLI migrations");
        b.UseNpgsql(conn);
    }
}
