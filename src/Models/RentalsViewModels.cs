using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodCarRentals.Models;

/// <summary>
/// This is to supports the process of renting a car, allowing users to select from available cars
/// and customers, and specify the rental timeframe.
/// </summary>
public class RentCarViewModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<SelectListItem> Customers { get; set; } = default!;

    public Guid CarId { get; set; }
    public List<SelectListItem> AvailableCars { get; set; } = default!;

    [DataType(DataType.DateTime)]
    [DisplayName("Rental Date")]
    public DateTime RentalDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayName("Expected End Date")]
    public DateTime ExpectedReturnDate { get; set; }

    [DisplayName("Total Cost")]
    [DataType(DataType.Currency)]
    [Range(10.00, 10000.00, ErrorMessage = "Value for {0} must be between {1:C} and {2:C}")]
    public required decimal TotalCost { get; set; }
}

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

/// <summary>
/// Used to report a car as returned.
/// </summary>
public class ReturnCarViewModel
{
    public Guid Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayName("Return Date")]
    public DateTime ReturnDate { get; set; }

    [DisplayName("KMs at rental")]
    public int KilometersAtRental { get; set; }

    [HiddenInput] // validation only
    public DateTime RentalDate { get; set; }
}
