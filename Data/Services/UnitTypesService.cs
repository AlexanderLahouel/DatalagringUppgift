
using Data.Interfaces;
namespace Data.Services;
public class UnitTypesService
{
    private readonly IUnitTypesRepository _repository;

    public UnitTypesService(IUnitTypesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UnitType>> GetAllUnitTypesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UnitType?> GetUnitTypeByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> AddUnitTypeAsync(UnitType unitType)
    {
        if (unitType == null) return false;

        return await _repository.AddAsync(unitType);
    }

    public async Task<bool> UpdateUnitTypeAsync(UnitType unitType)
    {
        if (unitType == null) return false;

        return await _repository.UpdateAsync(unitType);
    }

    public async Task<bool> DeleteUnitTypeAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
