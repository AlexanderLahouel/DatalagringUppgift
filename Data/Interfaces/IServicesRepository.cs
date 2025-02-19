using Data.Entities;

namespace Data.Interfaces;

public interface IServicesRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(int serviceId);
    Task<Service?> GetByNameAsync(string serviceName);
    Task<Service> AddOrGetServiceAsync(string serviceName, decimal price, int unitTypeId);
    Task AddAsync(Service service);
    Task UpdateAsync(Service service);
    Task DeleteAsync(int serviceId);
}


