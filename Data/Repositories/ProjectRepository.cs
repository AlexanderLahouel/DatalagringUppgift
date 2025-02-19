using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Interfaces;


namespace Data.Repositories
{
    public class ProjectRepository : IProjectRepository
        //Mycket AI-genererat igen.
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync() //Hämtar projekt med relaterade entiteter och returnerar en lista med projekt.
        {
            return await _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.Customer)
                .Include(p => p.Service)
                .Include(p => p.StatusType)
                .ToListAsync();
        }


        public async Task<Project?> GetByIdAsync(int id) //Samma sak fast den här letar efter specifika projekt efter id.
        {
            return await _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.Customer)
                .Include(p => p.Service) 
                    .ThenInclude(s => s.UnitType) 
                .Include(p => p.StatusType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task AddAsync(Project project)
            //Lägger till projekt i databasen och skapar nya saker i databasen om mina inputs inte funnits i databasen innan.
        {
          
            if (project.Service != null)
            {
                var existingUnitType = await _context.UnitTypes.FindAsync(project.Service.UnitTypeId);
                if (existingUnitType == null)
                {
                    existingUnitType = new UnitType { UnitName = "Default Type" }; 
                    _context.UnitTypes.Add(existingUnitType);
                    await _context.SaveChangesAsync();

                    project.Service.UnitTypeId = existingUnitType.Id;
                }
            }

           
            if (project.Service != null)
            {
                var existingService = await _context.Services
                    .FirstOrDefaultAsync(s => s.Name == project.Service.Name);
                if (existingService == null)
                {
                    _context.Services.Add(project.Service);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    project.Service = existingService; 
                }
            }

            
            if (project.Customer != null)
            {
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.CustomerName == project.Customer.CustomerName);
                if (existingCustomer == null)
                {
                    _context.Customers.Add(project.Customer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    project.Customer = existingCustomer;
                }
            }

          
            if (project.ProjectManager != null)
            {
                var existingManager = await _context.Employees
                    .FirstOrDefaultAsync(e => e.FirstName == project.ProjectManager.FirstName &&
                                              e.LastName == project.ProjectManager.LastName);
                if (existingManager == null)
                {
                    _context.Employees.Add(project.ProjectManager);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    project.ProjectManager = existingManager; 
                }
            }

            
            var existingStatus = await _context.StatusTypes.FindAsync(project.StatusTypeId);
            if (existingStatus == null)
            {
                throw new InvalidOperationException("StatusTypeId är ogiltigt. Se till att status finns i databasen.");
            }
            project.Id = 0;
          
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Project project)  //Uppdaterar befintligt projekt och sparar ändringar i databasen.
        {
            var existing = await _context.Projects
                .Include(p => p.ProjectManager)
                .Include(p => p.Customer)
                .Include(p => p.Service)
                .Include(p => p.StatusType)
                .FirstOrDefaultAsync(p => p.Id == project.Id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(project);

          
                if (existing.ProjectManager != null)
                    _context.Entry(existing.ProjectManager).CurrentValues.SetValues(project.ProjectManager);

                if (existing.Customer != null)
                    _context.Entry(existing.Customer).CurrentValues.SetValues(project.Customer);

                if (existing.Service != null)
                    _context.Entry(existing.Service).CurrentValues.SetValues(project.Service);

                await _context.SaveChangesAsync();
            }
        }



        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateProjectNumberAsync() //Genererar ett unikt projektnummer genom att hämta senaste projektet och lägger till +1 siffra för varje nytt.
        {
            var lastProject = await _context.Projects.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            int nextId = lastProject != null ? int.Parse(lastProject.ProjectNumber.Split('-')[1]) + 1 : 101;
            return $"P-{nextId}";
        }
        public async Task<Customer?> GetCustomerByNameAsync(string name) //Hämtar en kund baserat på namn.
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == name);
        }

        public async Task<Employee?> GetEmployeeByNameAsync(string firstName) //Hämtar anställd baserat på förnamn.
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.FirstName == firstName);
        }
        public async Task<List<StatusType>> GetAllStatusTypesAsync()
        {
            return await _context.StatusTypes.ToListAsync();
        }

    }
}



