using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodCarRentals.Models;

public class CarViewModel
{
    public Guid Id { get; set; }

    [Required]
    public required string Make { get; set; }

    [Required]
    public required string Model { get; set; }

    [Required]
    public required string Year { get; set; }

    public int Kilometers { get; set; } = default!;

    [Required]
    [DisplayName("Is Rented?")]
    public bool IsRented { get; set; } = false;

    [Required]
    [DisplayName("License Plate")]
    public string LicensePlate { get; set; } = null!;
    public ICollection<RentalDetailsViewModel> Rentals { get; set; } = new List<RentalDetailsViewModel>();

    [DisplayName("Car")]
    public string FullName => $"{Make} {Model}";
}

