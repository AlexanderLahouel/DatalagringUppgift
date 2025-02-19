using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Interfaces;

public class StatusTypeRepository : IStatusTypeRepository
{
    private readonly ApplicationDbContext _context;

    public StatusTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StatusType>> GetAllAsync()
    {
        return await _context.StatusTypes
            .AsNoTracking() 
            .ToListAsync();
    }

    public async Task<StatusType?> GetByIdAsync(int id)
    {
        return await _context.StatusTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(st => st.Id == id);
    }

    public async Task<bool> AddAsync(StatusType statusType)
    {
        if (statusType == null) return false;

        _context.StatusTypes.Add(statusType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(StatusType statusType)
    {
        if (statusType == null) return false;

        var existingStatusType = await _context.StatusTypes.FindAsync(statusType.Id);
        if (existingStatusType == null) return false;

        _context.Entry(existingStatusType).CurrentValues.SetValues(statusType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var statusType = await _context.StatusTypes.FindAsync(id);
        if (statusType == null) return false;

        _context.StatusTypes.Remove(statusType);
        await _context.SaveChangesAsync();
        return true;
    }
}


