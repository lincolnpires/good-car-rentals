using GoodCarRentals.Data;
using GoodCarRentals.Data.Models;
using GoodCarRentals.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GoodCarRentals.Application;

public class RentalsService(CarRentalsContext dbContext)
{
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
            RentalDate = rentCarViewModel.RentalDate,
            ExpectedReturnDate = rentCarViewModel.ExpectedReturnDate,
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
        rental.RentalDate = rentCarViewModel.RentalDate;
        rental.TotalCost = rentCarViewModel.TotalCost;
        rental.ExpectedReturnDate = rentCarViewModel.ExpectedReturnDate;

        dbContext.Rentals.Update(rental);
        await dbContext.SaveChangesAsync();
        return MapToDetails(rental);
    }

    public async Task<RentalDetailsViewModel> ReturnRental(ReturnCarViewModel returningRental)
    {
        var rental = await dbContext.Rentals.FindAsync(returningRental.Id);
        if (rental == null)
        {
            throw new InvalidOperationException("Rental not found");
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
