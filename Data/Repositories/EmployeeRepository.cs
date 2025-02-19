using Data.Entities;
using Data.Interfaces;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByNameAsync(string firstName)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == firstName);
    }

    public async Task<Employee> AddOrGetEmployeeAsync(string firstName)
    {
        var existingEmployee = await GetByNameAsync(firstName);
        if (existingEmployee != null)
        {
            return existingEmployee; 
        }

        var newEmployee = new Employee { FirstName = firstName };
        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync();

        return newEmployee; 
    }

    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await GetByIdAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}


