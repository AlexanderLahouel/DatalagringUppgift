public class ServicesRepository : IServicesRepository
{
    private readonly ApplicationDbContext _context;

    public ServicesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Service>> GetAllServices()
    {
        return await _context.Services.Include(s => s.UnitType).ToListAsync();
    }

    public async Task<Service?> GetServiceById(int serviceId)
    {
        return await _context.Services.Include(s => s.UnitType)
            .FirstOrDefaultAsync(s => s.Id == serviceId);
    }

    public async Task AddService(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateService(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteService(int serviceId)
    {
        var service = await _context.Services.FindAsync(serviceId);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
