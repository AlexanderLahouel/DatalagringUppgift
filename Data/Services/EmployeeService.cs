using Data.Entities;
using Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _employeeRepository.AddAsync(employee);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _employeeRepository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new ArgumentException("Employee not found.");
        }
        if (employee.Projects.Any())
        {
            throw new InvalidOperationException("Cannot delete an employee assigned to projects.");
        }
        await _employeeRepository.DeleteAsync(id);
    }
}
