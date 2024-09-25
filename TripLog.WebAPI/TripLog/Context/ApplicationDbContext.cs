using Microsoft.EntityFrameworkCore;
using TripLog.Models;

namespace TripLog.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripPhoto> TripPhotos { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(
            new
            {
                Name : "trabzon",
            });
    }*/
}

