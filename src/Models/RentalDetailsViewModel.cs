using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodCarRentals.Models;

/// <summary>
/// The details of a rental, for display/reading purposes.
/// </summary>
public class RentalDetailsViewModel
{
    public Guid Id { get; set; }

    [DisplayName("Customer")]
    public string CustomerName { get; set; } = null!;

    [DisplayName("Car")]
    public string CarDetails { get; set; } = null!;

    [DataType(DataType.Date)]
    [DisplayName("Rental Date")]
    public DateTime RentalDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayName("Expected End Date")]
    public DateTime? ReturnDate { get; set; }

    [DisplayName("KMs")]
    public int? KilometersAtRental { get; set; }

    [DisplayName("Returned?")]
    public bool IsReturned { get; set; }

    [DisplayName("Total Cost")]
    [DataType(DataType.Currency)]
    public required decimal TotalCost { get; set; }
}
