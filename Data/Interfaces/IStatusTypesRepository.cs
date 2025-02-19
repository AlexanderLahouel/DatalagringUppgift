
namespace Data.Interfaces;

public interface IStatusTypeRepository
{
    Task<IEnumerable<StatusType>> GetAllAsync();
    Task<StatusType?> GetByIdAsync(int id);
    Task<bool> AddAsync(StatusType statusType);
    Task<bool> UpdateAsync(StatusType statusType);
    Task<bool> DeleteAsync(int id);
}
