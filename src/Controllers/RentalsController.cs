using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodCarRentals.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodCarRentals.Data;
using GoodCarRentals.Data.Models;
using GoodCarRentals.Models;

namespace GoodCarRentals.Controllers
{
    public class RentalsController(RentalsService rentalsService) : Controller
    {
        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            return View(await rentalsService.GetAllRentals());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await rentalsService.GetRentalDetailsById(id.Value);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("RentalDate,ReturnDate,TotalCost,IsReturned")] RentCarViewModel rental)
        {
            if (!ModelState.IsValid) return View(rental);
            await rentalsService.CreateRental(rental);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await rentalsService.GetRentalById(id.Value);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,RentalDate,ReturnDate,TotalCost,IsReturned")] RentCarViewModel rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(rental); // validation errors

            try
            {
                var updatedRental = await rentalsService.UpdateRental(rental);
                if (updatedRental == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
                // log this!
            }
        }

        // POST: Rentals/ReturnCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnCar(Guid id,
            [Bind("Id,ReturnDate,KilometersAtRental")] ReturnCarViewModel returnCarViewModel)
        {
            if (id != returnCarViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(returnCarViewModel); // validation errors

            try
            {
                var updatedRental = await rentalsService.ReturnRental(returnCarViewModel);
                if (updatedRental == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
                // log as well!
            }
        }

        // TODO: maybe use for cancelations
        // GET: Rentals/Delete/5
        // public async Task<IActionResult> Delete(Guid? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var rental = await _context.Rentals
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (rental == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(rental);
        // }

        // POST: Rentals/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(Guid id)
        // {
        //     var rental = await _context.Rentals.FindAsync(id);
        //     if (rental != null)
        //     {
        //         _context.Rentals.Remove(rental);
        //     }
        //
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

    }
}
