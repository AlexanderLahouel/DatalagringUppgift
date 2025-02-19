using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;


    public class CustomerContactsService
    {
        private readonly ICustomerContactsRepository _customerContactsRepository;

        public CustomerContactsService(ICustomerContactsRepository customerContactsRepository)
        {
            _customerContactsRepository = customerContactsRepository;
        }

        public async Task<IEnumerable<CustomerContacts>> GetAllContactsAsync()
        {
            return await _customerContactsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CustomerContacts>> GetContactsByCustomerIdAsync(int customerId)
        {
            return await _customerContactsRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<CustomerContacts?> GetContactByIdAsync(string firstName, string lastName, int customerId)
        {
            return await _customerContactsRepository.GetByIdAsync(firstName, lastName, customerId);
        }

        public async Task AddContactAsync(CustomerContacts contact)
        {
            await _customerContactsRepository.AddAsync(contact);
        }

        public async Task UpdateContactAsync(CustomerContacts contact)
        {
            await _customerContactsRepository.UpdateAsync(contact);
        }

        public async Task DeleteContactAsync(string firstName, string lastName, int customerId)
        {
            await _customerContactsRepository.DeleteAsync(firstName, lastName, customerId);
        }
    }
