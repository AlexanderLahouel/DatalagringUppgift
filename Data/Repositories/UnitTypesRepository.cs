using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Interfaces;

public class UnitTypesRepository : IUnitTypesRepository
{
    private readonly ApplicationDbContext _context;

    public UnitTypesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UnitType>> GetAllAsync()
    {
        return await _context.UnitTypes
            .AsNoTracking() 
            .ToListAsync();
    }

    public async Task<UnitType?> GetByIdAsync(int id)
    {
        return await _context.UnitTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(ut => ut.Id == id);
    }

    public async Task<bool> AddAsync(UnitType unitType)
    {
        if (unitType == null) return false;

        _context.UnitTypes.Add(unitType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(UnitType unitType)
    {
        if (unitType == null) return false;

        var existingUnitType = await _context.UnitTypes.FindAsync(unitType.Id);
        if (existingUnitType == null) return false;

        _context.Entry(existingUnitType).CurrentValues.SetValues(unitType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var unitType = await _context.UnitTypes.FindAsync(id);
        if (unitType == null) return false;

        _context.UnitTypes.Remove(unitType);
        await _context.SaveChangesAsync();
        return true;
    }
}

