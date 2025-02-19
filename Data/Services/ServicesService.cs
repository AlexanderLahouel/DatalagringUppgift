using Data.Entities;
using Data.Interfaces;



public class ServicesService
{
    private readonly IServicesRepository _repository;

    public ServicesService(IServicesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _repository.GetByIdAsync(serviceId);
    }

    public async Task<bool> AddServiceAsync(Service service)
    {
        if (service == null) return false;

        await _repository.AddAsync(service);
        return true;
    }

    public async Task<bool> UpdateServiceAsync(Service service)
    {
        if (service == null) return false;

        await _repository.UpdateAsync(service);
        return true;
    }

    public async Task<bool> DeleteServiceAsync(int serviceId)
    {
        var existingService = await _repository.GetByIdAsync(serviceId);
        if (existingService == null) return false;

        await _repository.DeleteAsync(serviceId);
        return true;
    }
}
