namespace GoodCarRentals.Data.Models;

public class Car
{
    public Guid Id { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public string Year { get; set; }
    public int Kilometers { get; set; } = default!;

    public bool IsRented { get; set; } = false;

    // maybe later
    // public string? Image { get; set; } = null;

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    // to display
    public string FullName => $"{Make} {Model}";
}
