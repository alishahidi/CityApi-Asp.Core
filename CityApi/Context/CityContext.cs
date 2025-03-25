using CityApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Context;

public class CityContext(DbContextOptions<CityContext> options) : DbContext(options)
{
    public DbSet<City> Cities { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "New York", Province = "New York" },
            new City { Id = 2, Name = "Los Angeles", Province = "California" },
            new City { Id = 3, Name = "Chicago", Province = "Illinois" }
        );
    }
}