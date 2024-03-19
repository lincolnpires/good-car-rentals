using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodCarRentals.Data.Models;

public class Rental
{
    public Guid Id { get; set; }
    public required Guid CarId { get; set; }
    public Car Car { get; set; } = default!;
    public required Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    [DataType(DataType.Date)]
    public required DateTime RentalDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public required decimal TotalCost { get; set; }

    public bool IsReturned { get; set; }

    public int? KilometersAtRental { get; set; }
}
