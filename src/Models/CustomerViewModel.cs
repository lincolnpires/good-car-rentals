namespace GoodCarRentals.Models;

public class CustomerViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Country { get; set; }

    public ICollection<RentalDetailsViewModel> Rentals { get; set; } = new List<RentalDetailsViewModel>();
}

