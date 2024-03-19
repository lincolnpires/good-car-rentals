using GoodCarRentals.Data;
using GoodCarRentals.Data.Models;
using GoodCarRentals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodCarRentals.Application;

public class RentalsService(CarRentalsContext dbContext)
{
    public async Task<RentCarViewModel> InitializeRentCarSelects()
    {
        var model = new RentCarViewModel();

        var customerSelect = await PopulateCustomerSelect();
        model.Customers = customerSelect;

        var carSelect = await PopulateCarSelect();
        model.AvailableCars = carSelect;

        return model;
    }

    public async Task<RentCarViewModel> InitializeRentCarSelects(RentCarViewModel model)
    {
        if (model == null)
        {
            throw new InvalidOperationException("Rental not found");
        }

        var customerSelect = await PopulateCustomerSelect();
        model.Customers = customerSelect;

        var carSelect = await PopulateCarSelect();
        model.AvailableCars = carSelect;

        return model;
    }

    private async Task<List<SelectListItem>> PopulateCarSelect()
    {
        var carSelect = new List<SelectListItem>();
        carSelect.Insert(0, new SelectListItem
        {
            Value = "", // empty
            Text = "Select a car"
        });
        var cars = await dbContext.Cars
            .Where(car => !car.IsRented)
            .Select(car => new SelectListItem
            {
                Value = car.Id.ToString("D"), // need hyphens LOL
                Text = car.Make + " " + car.Model + " " + car.Year
            })
            .ToListAsync();
        carSelect.AddRange(cars);
        return carSelect;
    }

    private async Task<List<SelectListItem>> PopulateCustomerSelect()
    {
        var customerSelect = await dbContext.Customers
            .Select(customer => new SelectListItem
            {
                Value = customer.Id.ToString("D"),
                Text = customer.Name
            })
            .ToListAsync();
        customerSelect.Insert(0, new SelectListItem
        {
            Value = "",
            Text = "Select a customer"
        });
        return customerSelect;
    }

    public async Task<IEnumerable<RentalDetailsViewModel>> GetAllRentals()
    {
        var rentals = await dbContext.Rentals
            .Include(rental => rental.Car)
            .Include(rental => rental.Customer)
            .ToListAsync();

        return rentals.Select(MapToDetails);
    }

    private RentalDetailsViewModel MapToDetails (Rental rental)
    {
        return new RentalDetailsViewModel
        {
            CarDetails = rental.Car.Make + " " + rental.Car.Model + " " + rental.Car.Year,
            CustomerName = rental.Customer.Name,
            RentalDate = rental.RentalDate,
            ReturnDate = rental.ReturnDate,
            TotalCost = rental.TotalCost,
            IsReturned = rental.IsReturned,
            KilometersAtRental = rental.KilometersAtRental,
            Id = rental.Id
        };
    }

    public async Task<RentalDetailsViewModel?> GetRentalDetailsById(Guid id)
    {
        var rental = await dbContext.Rentals
            .Include(rental => rental.Car)
            .Include(rental => rental.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        return rental != null ? MapToDetails(rental) : null;
    }

    public async Task<RentCarViewModel?> GetRentalById(Guid id)
    {
        var rental = await dbContext.Rentals
            .Include(rental => rental.Car)
            .Include(rental => rental.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        return rental != null ? MapToEdit(rental) : null;
    }

    private RentCarViewModel MapToEdit(Rental rental)
    {
        return new RentCarViewModel
        {
            Id = rental.Id,
            CarId = rental.CarId,
            CustomerId = rental.CustomerId,
            RentalDate = rental.RentalDate,
            ExpectedReturnDate = rental.ExpectedReturnDate,
            TotalCost = rental.TotalCost
        };
    }

    public async Task<RentCarViewModel> CreateRental(RentCarViewModel rentCarViewModel)
    {
        var rental = MapToRental(rentCarViewModel);
        dbContext.Rentals.Add(rental);
        await dbContext.SaveChangesAsync();
        return MapToEdit(rental);
    }

    private static Rental MapToRental(RentCarViewModel rentCarViewModel)
    {
        var rental = new Rental
        {
            CarId = rentCarViewModel.CarId,
            CustomerId = rentCarViewModel.CustomerId,
            RentalDate = rentCarViewModel.RentalDate.Value,
            ExpectedReturnDate = rentCarViewModel.ExpectedReturnDate.Value,
            TotalCost = rentCarViewModel.TotalCost
        };
        return rental;
    }

    public async Task<RentalDetailsViewModel> UpdateRental(RentCarViewModel rentCarViewModel)
    {
        var rental = await dbContext.Rentals.FindAsync(rentCarViewModel.Id);
        if (rental == null)
        {
            return null;
        }

        // Not getting too much into details, but just assuming that (besides existing),
        // the car and customer could be changed because of a mistake or something.
        // also the cost and rental date.
        // What I would not let be here is the return data.
        rental.CarId = rentCarViewModel.CarId;
        rental.CustomerId = rentCarViewModel.CustomerId;
        rental.RentalDate = rentCarViewModel.RentalDate.Value;
        rental.TotalCost = rentCarViewModel.TotalCost;
        rental.ExpectedReturnDate = rentCarViewModel.ExpectedReturnDate.Value;

        dbContext.Rentals.Update(rental);
        await dbContext.SaveChangesAsync();
        return MapToDetails(rental);
    }

    public async Task<RentalDetailsViewModel?> ReturnRental(ReturnCarViewModel returningRental)
    {
        var rental = await dbContext.Rentals.FindAsync(returningRental.Id);
        if (rental == null)
        {
            // throw new InvalidOperationException("Rental not found"); // just log?
            return null;
        }
        if (rental.IsReturned)
        {
            throw new InvalidOperationException("Rental already returned");
        }
        var car = await dbContext.Cars.FindAsync(rental.CarId);
        if (car == null)
        {
            throw new InvalidOperationException("Car not found");
        }
        // In a DDD approach, we would have those in the respective entities (Car and Rental)
        // Still, I'll KISS here - for the sake of the example and understanding all at once.
        car.Kilometers += returningRental.KilometersAtRental;
        car.IsRented = false;
        rental.IsReturned = true;
        rental.ReturnDate = returningRental.ReturnDate;
        rental.KilometersAtRental = returningRental.KilometersAtRental;
        // TODO: add something to say the customer is nice to return on time - or not (o_o)

        dbContext.Rentals.Update(rental);
        await dbContext.SaveChangesAsync();
        return MapToDetails(rental);
    }

    private ReturnCarViewModel MapToReturn(Rental rental)
    {
        return new ReturnCarViewModel
        {
            Id = rental.Id,
            ReturnDate = rental.ReturnDate.GetValueOrDefault(DateTime.Now),
            KilometersAtRental = rental.KilometersAtRental.GetValueOrDefault(0)
        };
    }
}
