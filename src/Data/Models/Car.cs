using System.ComponentModel.DataAnnotations;

namespace GoodCarRentals.Data.Models;

public class Car
{
    public Guid Id { get; set; }

    [StringLength(40)]
    public required string Make { get; set; }

    [StringLength(40)]
    public required string Model { get; set; }
    public required int Year { get; set; }
    public int Kilometers { get; set; } // think the name of this is odometre/odometer
    public bool IsRented { get; set; } // keep this for simplicity

    [StringLength(10)]
    public string PlateNumber { get; set; } = null!;

    // TODO: maybe later add Image

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
