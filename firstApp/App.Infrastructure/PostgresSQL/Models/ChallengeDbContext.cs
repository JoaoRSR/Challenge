using App.Infrastructure.PostgresSQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.PostgresSQL.Models;

public class ChallengeDbContext : DbContext
{
    protected readonly PgConfiguration _configuration;

    public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options) { }

    public DbSet<UsersActivityCountEntity> UsersActivityCount { get; set; }

    public DbSet<PostsEntity> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}
