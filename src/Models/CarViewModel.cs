namespace GoodCarRentals.Models;

public class CarViewModel
{
    public Guid Id { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required string Year { get; set; }
    public int Kilometers { get; set; } = default!;

    public bool IsRented { get; set; } = false;
    public string LicensePlate { get; set; }
    public ICollection<RentalDetailsViewModel> Rentals { get; set; } = new List<RentalDetailsViewModel>();

    // to display
    public string FullName => $"{Make} {Model}";
}

