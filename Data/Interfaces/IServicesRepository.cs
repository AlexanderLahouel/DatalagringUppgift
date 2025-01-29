public interface IServicesRepository
{
    Task<List<Service>> GetAllServices();
    Task<Service?> GetServiceById(int serviceId);
    Task AddService(Service service);
    Task UpdateService(Service service);
    Task DeleteService(int serviceId);
}

