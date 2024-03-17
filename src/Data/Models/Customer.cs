namespace GoodCarRentals.Data.Models;

public class Customer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Country { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
