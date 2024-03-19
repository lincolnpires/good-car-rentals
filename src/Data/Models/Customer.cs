using System.ComponentModel.DataAnnotations;

namespace GoodCarRentals.Data.Models;

public class Customer
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(100)]
    public required string Email { get; set; }

    [StringLength(25)]
    public required string Phone { get; set; }

    [StringLength(255)]
    public required string Address { get; set; }

    [StringLength(100)]
    public required string Country { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
