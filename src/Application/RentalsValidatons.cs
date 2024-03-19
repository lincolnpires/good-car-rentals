using FluentValidation;
using System;
using GoodCarRentals.Data;
using GoodCarRentals.Models;

namespace GoodCarRentals.Application;

public class RentCarValidator : AbstractValidator<RentCarViewModel>
{
    public RentCarValidator(CarRentalsContext dbContext)
    {
        RuleFor(rental => rental.CustomerId)
            .NotEmpty();
        RuleFor(rental => rental.CarId)
            .NotEmpty();
        RuleFor(rental => rental.RentalDate)
            .NotEmpty()
            .LessThan(rental => rental.ExpectedReturnDate)
            .WithMessage("Start date must be before end date");
        RuleFor(rental => rental.ExpectedReturnDate)
            .NotEmpty()
            .GreaterThan(rental => rental.RentalDate)
            .WithMessage("End date must be after start date");
        RuleFor(rental => rental.TotalCost)
            .GreaterThanOrEqualTo(10)
            .LessThanOrEqualTo(10000);
        // .WithMessage("Total cost must be between 10 and 10000");

        // WARN - Especial case: the car must be available for rental
        RuleFor(x => x).MustAsync(async (carRental, cancellationToken) =>
        {
            var car = await dbContext.Cars
                .FindAsync(new object?[] { carRental.CarId }, cancellationToken: cancellationToken);
            return car is { IsRented: true };
        }).WithMessage("Car is already rented");

        // depending on the business, maybe the customer can only have a certain number of rentals as well...
        // skipping that for now
    }
}

public class ReturnCarValidator : AbstractValidator<ReturnCarViewModel>
{
    public ReturnCarValidator()
    {
        RuleFor(rental => rental.Id)
            .NotEmpty();
        RuleFor(rental => rental.ReturnDate)
            .NotEmpty();
        RuleFor(rental => rental.ReturnDate)
            .GreaterThanOrEqualTo(rental => rental.RentalDate)
            .WithMessage("End date must be after start date");
        RuleFor(rental => rental.KilometersAtRental)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilometers must be a positive number");
    }
}
