using Data.Entities;
using Data.Interfaces;


public class StatusTypesService
{
    private readonly IStatusTypeRepository _repository;

    public StatusTypesService(IStatusTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> AddStatusTypeAsync(StatusType statusType)
    {
        if (statusType == null) return false;

        await _repository.AddAsync(statusType); 
        return true;
    }
}