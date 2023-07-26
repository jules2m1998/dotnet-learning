using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext dbContext;

    public WalkRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();

        return walk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walk = await GetByIdAsync(id);
        if (walk == null) return null;

        dbContext.Walks.Remove(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync(
        string? filterOn, 
        string? filterQuery, 
        string? sortBy, 
        bool isAscending = true, 
        int pageNumber = 0,
        int pageSize = 0)
    {
        var walks = dbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();
        if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                walks = walks.Where(w => w.Name.Contains(filterQuery));
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name) ;
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                walks = isAscending ? walks.OrderBy(x => x.LenghtInKm) : walks.OrderByDescending(x => x.LenghtInKm) ;
        }

        var skipResults = (pageNumber - 1) * pageSize;

        return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id) => 
        await dbContext
        .Walks
        .Include(w => w.Difficulty)
        .Include(w => w.Region)
        .FirstOrDefaultAsync(w => w.Id == id);

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var current = await dbContext.Walks.FindAsync(id);
        if (current == null) return null;
        current.Name = walk.Name;
        current.Description = walk.Description;
        current.LenghtInKm = walk.LenghtInKm;
        current.WalkImageUrl = walk.WalkImageUrl;
        current.DifficultyId = walk.DifficultyId;
        current.RegionId = walk.RegionId;

        await dbContext.SaveChangesAsync();
        return current;
    }
}
