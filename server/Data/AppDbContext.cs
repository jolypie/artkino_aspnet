using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<UserFilmRelation> UserFilmRelations => Set<UserFilmRelation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserFilmRelation>
            .Property(e => e.Type)
            .HasConversation<string>();
    }
}
