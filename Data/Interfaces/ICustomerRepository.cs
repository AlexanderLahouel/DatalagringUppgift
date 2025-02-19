using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int customerId);
    Task<Customer?> GetByNameAsync(string customerName);  
    Task<Customer> AddOrGetCustomerAsync(string customerName); 
    Task AddAsync(Customer customer); 
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int customerId);
    Task<bool> ExistsAsync(int customerId);
}

