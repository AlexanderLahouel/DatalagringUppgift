using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerContactsRepository
{
    Task<IEnumerable<CustomerContacts>> GetAllAsync();
    Task<IEnumerable<CustomerContacts>> GetByCustomerIdAsync(int customerId);
    Task<CustomerContacts?> GetByIdAsync(string firstName, string lastName, int customerId);
    Task AddAsync(CustomerContacts contact);
    Task UpdateAsync(CustomerContacts contact);
    Task DeleteAsync(string firstName, string lastName, int customerId);
}

