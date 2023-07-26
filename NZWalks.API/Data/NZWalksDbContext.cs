namespace NZWalks.API.Data;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Configurations;
using NZWalks.API.Models.Domain;

public class NZWalksDbContext : DbContext
{
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Image> Images { get; set; }
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DifficultyModelConfiguration).Assembly);
        modelBuilder.ApplyConfiguration(new DifficultyModelConfiguration());
        modelBuilder.ApplyConfiguration(new RegionModelconfiguration());
    }
}
