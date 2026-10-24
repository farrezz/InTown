using InTown.Data.Configuration;
using InTown.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace InTown.Data;
public class AppDbContext : DbContext
{
    // the settings (which database file) come in from program.cs
    
    // Convert into primary constructor ?
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    //one DbSet per table
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Attendee> Attendees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventConfiguration());
    }
}