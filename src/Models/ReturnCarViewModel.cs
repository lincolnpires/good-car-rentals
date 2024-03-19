using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GoodCarRentals.Models;

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
