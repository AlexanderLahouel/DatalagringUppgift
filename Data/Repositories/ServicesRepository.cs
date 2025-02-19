using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly Contexts.ApplicationDbContext _context;

        public ServicesRepository(Contexts.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services
                .Include(s => s.UnitType)
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int serviceId) //Hämtar en specifik tjänst från databasen baserat på serviceId
        {
            return await _context.Services
                .Include(s => s.UnitType)
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<Service?> GetByNameAsync(string serviceName)
        {
            return await _context.Services
                .Include(s => s.UnitType)
                .FirstOrDefaultAsync(s => s.Name == serviceName);
        }

     
        // Hämtar en tjänst om den redan finns, annars skapar den en ny.
       
        public async Task<Service> AddOrGetServiceAsync(string serviceName, decimal price, int unitTypeId)
        {
            // Kontrollera om tjänsten redan finns
            var existingService = await GetByNameAsync(serviceName);
            if (existingService != null)
            {
                return existingService;
            }

            // Kontrollerar om unittype finns
            var existingUnitType = await _context.UnitTypes.FindAsync(unitTypeId);
            if (existingUnitType == null)
            {
                // Om den inte finns, skapar en standard UnitType
                existingUnitType = new UnitType { UnitName = "Standard" };
                _context.UnitTypes.Add(existingUnitType);
                await _context.SaveChangesAsync();
            }

            // Skapa tjänsten
            var newService = new Service { Name = serviceName, Price = price, UnitTypeId = existingUnitType.Id };
            _context.Services.Add(newService);
            await _context.SaveChangesAsync();

            return newService;
        }


        public async Task AddAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            var existingService = await _context.Services.FindAsync(service.Id);
            if (existingService != null)
            {
                existingService.Name = service.Name;
                existingService.Price = service.Price;

                var unitTypeExists = await _context.UnitTypes.AnyAsync(u => u.Id == service.UnitTypeId);
                if (unitTypeExists)
                {
                    existingService.UnitTypeId = service.UnitTypeId;
                }
                else
                {
                    throw new InvalidOperationException("UnitTypeId är ogiltigt.");
                }

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int serviceId)
        {
            var service = await GetByIdAsync(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}


