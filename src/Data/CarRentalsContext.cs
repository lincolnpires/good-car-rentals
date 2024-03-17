using GoodCarRentals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodCarRentals.Data;

public class CarRentalsContext: DbContext
{
    public DbSet<Car> Cars { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Rental> Rentals { get; set; } = default!;

    private string DbPath { get; }

    public CarRentalsContext(DbContextOptions<CarRentalsContext> options) : base(options)
    {
        var appPath = AppContext.BaseDirectory;
        DbPath = Path.Combine(appPath, "GoodCarRentals.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

}
