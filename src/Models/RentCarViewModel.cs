using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodCarRentals.Models;

/// <summary>
/// This is to supports the process of renting a car, allowing users to select from available cars
/// and customers, and specify the rental timeframe.
/// </summary>
public class RentCarViewModel
{
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }
    public List<SelectListItem> Customers { get; set; } = default!;

    [Required]
    public Guid CarId { get; set; }
    public List<SelectListItem> AvailableCars { get; set; } = default!;

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayName("Rental Date")]
    public DateTime? RentalDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayName("Expected End Date")]
    public DateTime? ExpectedReturnDate { get; set; }

    [Required]
    [DisplayName("Total Cost")]
    [DataType(DataType.Currency)]
    [Range(10.00, 10000.00, ErrorMessage = "Value for {0} must be between {1:C} and {2:C}")]
    public decimal TotalCost { get; set; }
}
