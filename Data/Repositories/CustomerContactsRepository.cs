using Data.Entities;
using Data.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerContactsRepository : ICustomerContactsRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerContactsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerContacts>> GetAllAsync()
    {
        return await _context.CustomerContacts.Include(cc => cc.Customer).ToListAsync();
    }

    public async Task<IEnumerable<CustomerContacts>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.CustomerContacts
            .Where(cc => cc.CustomerId == customerId)
            .Include(cc => cc.Customer)
            .ToListAsync();
    }

    public async Task<CustomerContacts?> GetByIdAsync(string firstName, string lastName, int customerId)
    {
        return await _context.CustomerContacts
            .FirstOrDefaultAsync(cc => cc.FirstName == firstName && cc.LastName == lastName && cc.CustomerId == customerId);
    }

    public async Task AddAsync(CustomerContacts contact)
    {
        _context.CustomerContacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CustomerContacts contact)
    {
        _context.CustomerContacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string firstName, string lastName, int customerId)
    {
        var contact = await GetByIdAsync(firstName, lastName, customerId);
        if (contact != null)
        {
            _context.CustomerContacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}

