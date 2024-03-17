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

    /// <summary>
    /// Removes everything from the database and seeds it with development data.
    /// </summary>
    public void SeedDevelopmentData()
    {
        if (Rentals.Any())
        {
            Rentals.RemoveRange(Rentals);
            SaveChanges();
        }
        if (Cars.Any())
        {
            Cars.RemoveRange(Cars);
            SaveChanges();
        }
        if (Customers.Any())
        {
            Customers.RemoveRange(Customers);
            SaveChanges();
        }

        var cars = new List<Car>
        {
            new() { Make = "Toyota", Model = "Corolla", Year = "2024", Kilometers = 1000 },
            new() { Make = "Honda", Model = "Civic", Year = "2023", Kilometers = 2000 },
            new() { Make = "Ford", Model = "Mustang", Year = "2022", Kilometers = 3000 },
            new() { Make = "Chevrolet", Model = "Malibu", Year = "2021", Kilometers = 4000 },
            new() { Make = "Nissan", Model = "Altima", Year = "2020", Kilometers = 5000 },
            new() { Make = "Toyota", Model = "Sienna", Year = "2019", Kilometers = 7000 },
            new() { Make = "Honda", Model = "City", Year = "2018", Kilometers = 9000 },
            new() { Make = "Ford", Model = "Fusion", Year = "2017", Kilometers = 10000 },
            new() { Make = "Chevrolet", Model = "Camaro", Year = "2016", Kilometers = 15000 },
            new() { Make = "Nissan", Model = "Rogue", Year = "2015", Kilometers = 20000 }
        };

        Cars.AddRange(cars);
        SaveChanges();

        var customers = new List<Customer>
        {
            new() { Name = "John Wick", Email = "john.wick@continental.com", Phone = "1234567890", Address = "Continental Hotel, New York", Country = "USA" },
            new() { Name = "Sarah Connor", Email = "sarah.connor@tech-com.org", Phone = "2345678901", Address = "Terminator Safe House, Los Angeles", Country = "USA" },
            new() { Name = "Ellen Ripley", Email = "ripley@weyland.co", Phone = "3456789012", Address = "Nostromo, Outer Space", Country = "USA" }
        };

        Customers.AddRange(customers);
        SaveChanges();

        var rentals = new List<Rental>
        {
            new() { Car = cars[3], Customer = customers[0], RentalDate = DateTime.Now.AddDays(-5), TotalCost = 1000 },
            new() { Car = cars[4], Customer = customers[1], RentalDate = DateTime.Now.AddDays(-2), TotalCost = 800 },
            new() { Car = cars[5], Customer = customers[2], RentalDate = DateTime.Now, TotalCost = 300 }
        };

        Rentals.AddRange(rentals);
        SaveChanges();
    }
}
