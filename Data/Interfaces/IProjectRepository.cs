using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
        Task<string> GenerateProjectNumberAsync();
        Task<Customer?> GetCustomerByNameAsync(string name);
        Task<Employee?> GetEmployeeByNameAsync(string firstName);
        Task<List<StatusType>> GetAllStatusTypesAsync();

    }
}
