using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces;

public interface IUnitTypesRepository
{
    Task<IEnumerable<UnitType>> GetAllAsync();
    Task<UnitType?> GetByIdAsync(int id);
    Task<bool> AddAsync(UnitType unitType);
    Task<bool> UpdateAsync(UnitType unitType);
    Task<bool> DeleteAsync(int id);
}
