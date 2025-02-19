using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _customerRepository.GetByIdAsync(customerId);
    }

    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        if (await _customerRepository.ExistsAsync(customer.CustomerId))
        {
            return false; 
        }

        await _customerRepository.AddAsync(customer);
        return true;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        if (!await _customerRepository.ExistsAsync(customer.CustomerId))
        {
            return false; 
        }

        await _customerRepository.UpdateAsync(customer);
        return true;
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        if (!await _customerRepository.ExistsAsync(customerId))
        {
            return false; 
        }

        await _customerRepository.DeleteAsync(customerId);
        return true;
    }
}
