using Data.Entities;

namespace YourProjectNamespace.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers(); 
        Customer? GetCustomerById(int id);
        void AddCustomer(Customer customer);  
        void UpdateCustomer(Customer customer); 
        void DeleteCustomer(int id);
        Task<List<Customer>> GetAllCustomersAsync(); 

    }
}
