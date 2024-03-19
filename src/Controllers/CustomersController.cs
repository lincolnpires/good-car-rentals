using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodCarRentals.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoodCarRentals.Models;

namespace GoodCarRentals.Controllers
{
    public class CustomersController(CustomerService customerService) : Controller
    {
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await customerService.GetAllCustomers());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // using the Bind attribute. ([Bind("bla")]
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Email,Phone,Address,Country")] CustomerViewModel customer)
        {
            if (!ModelState.IsValid) return View(customer);

            var newCustomer = await customerService.CreateCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Email,Phone,Address,Country")] CustomerViewModel customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(customer); // with validation errors

            try
            {
                var updatedCustomer = await customerService.UpdateCustomer(customer);
                if (updatedCustomer == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerService.GetCustomerById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = await customerService.DeleteCustomer(id);
            if (!customer)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // private bool CustomerExists(Guid id)
        // {
        //     return customerService.Customers.Any(e => e.Id == id);
        // }
    }
}
