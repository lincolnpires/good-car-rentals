using GoodCarRentals.Data.Models;

namespace GoodCarRentals.Application;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodCarRentals.Data;
using GoodCarRentals.Models;

public class CustomerService(CarRentalsContext dbContext)
{
    public async Task<IEnumerable<CustomerViewModel>> GetAllCustomers()
    {
        var customers = await dbContext.Customers.ToListAsync();
        return customers.Select(MapToViewModel);
    }

    public async Task<CustomerViewModel?> GetCustomerById(Guid id)
    {
        var customer = await dbContext.Customers.FindAsync(id);
        return customer != null ? MapToViewModel(customer): null;
    }

    public async Task<CustomerViewModel> CreateCustomer(CustomerViewModel customerViewModel)
    {
        var customer = new Customer
        {
            Address = customerViewModel.Address,
            Country = customerViewModel.Country,
            Email = customerViewModel.Email,
            Name = customerViewModel.Name,
            Phone = customerViewModel.Phone
        };
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
        return MapToViewModel(customer);
    }

    public async Task<CustomerViewModel> UpdateCustomer(CustomerViewModel customerViewModel)
    {
        var customer = await dbContext.Customers.FindAsync(customerViewModel.Id);
        if (customer == null)
        {
            return null;
        }
        UpdateCustomer(customerViewModel, customer);
        dbContext.Customers.Update(customer);
        await dbContext.SaveChangesAsync();
        return MapToViewModel(customer);
    }

    private static CustomerViewModel MapToViewModel(Customer customer)
    {
        return new CustomerViewModel
        {
            Address = customer.Address,
            Country = customer.Country,
            Email = customer.Email,
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone
        };
    }

    private static void UpdateCustomer(CustomerViewModel customerViewModel, Customer customer)
    {
        customer.Address = customerViewModel.Address;
        customer.Country = customerViewModel.Country;
        customer.Email = customerViewModel.Email;
        customer.Name = customerViewModel.Name;
        customer.Phone = customerViewModel.Phone;
    }

    public async Task<bool> DeleteCustomer(Guid id)
    {
        var customer = await dbContext.Customers.FindAsync(id);
        if (customer == null)
        {
            return false;
        }
        dbContext.Customers.Remove(customer);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
