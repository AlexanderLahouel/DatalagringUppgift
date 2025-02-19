using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly Contexts.ApplicationDbContext _context;

    public CustomerRepository(Contexts.ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<Customer?> GetByNameAsync(string customerName)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == customerName);
    }

    public async Task<Customer> AddOrGetCustomerAsync(string customerName)
    {
        var existingCustomer = await GetByNameAsync(customerName);
        if (existingCustomer != null)
        {
            return existingCustomer;  
        }

        var newCustomer = new Customer { CustomerName = customerName };
        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();

        return newCustomer; 
    }

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int customerId)
    {
        var customer = await GetByIdAsync(customerId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int customerId)
    {
        return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
    }
}

