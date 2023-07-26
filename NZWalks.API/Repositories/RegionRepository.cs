using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly NZWalksDbContext _dbContext;

    public RegionRepository(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _dbContext.Regions.AddAsync(region);
        await _dbContext.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> DeletAsync(Guid id)
    {
        var region = await GetByIdAsync(id);
        if (region == null) return null;
        _dbContext.Regions.Remove(region);
        await _dbContext.SaveChangesAsync();
        return region;
    }

    public async Task<ICollection<Region>> GetAllAsync() => 
        await _dbContext.Regions.ToListAsync();

    public async Task<Region?> GetByIdAsync(Guid id) => 
        await _dbContext.Regions.FindAsync(id);

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await GetByIdAsync(id);
        if (existingRegion == null) return null;

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await _dbContext.SaveChangesAsync();

        return existingRegion;
    }
}
