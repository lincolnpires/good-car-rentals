using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodCarRentals.Models;

/// <summary>
/// This is to supports the process of renting a car, allowing users to select from available cars
/// and customers, and specify the rental timeframe.
/// </summary>
public class RentCarViewModel
{
    public Guid CustomerID { get; set; }
    public IEnumerable<CustomerViewModel> Customers { get; set; }

    public Guid CarID { get; set; }
    public IEnumerable<CarViewModel> AvailableCars { get; set; }

    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayName("Expected End Date")]
    public DateTime? ReturnDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public required decimal TotalCost { get; set; }
}

/// <summary>
/// The details of a rental, for display/reading purposes.
/// </summary>
public class RentalDetailsViewModel
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; }
    public string CarDetails { get; set; }
    public DateTime RentalDate { get; set; }
    [DisplayName("Expected End Date")]
    public DateTime? ReturnDate { get; set; }
    public int? KilometersRented { get; set; }
    public bool IsReturned { get; set; }
}

/// <summary>
/// Used to report a car as returned.
/// </summary>
public class ReturnCarViewModel
{
    public Guid Id { get; set; }
    [DataType(DataType.Date)]
    [DisplayName("Return Date")]
    public DateTime ReturnDate { get; set; }
    public int KilometersRented { get; set; }
}
