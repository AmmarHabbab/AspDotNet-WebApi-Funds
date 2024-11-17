using Microsoft.EntityFrameworkCore;
using WebApi1.DbContexts;
using WebApi1.Entities;

namespace WebApi1.Services;

public class CityInforepository : ICityInfoRepository
{
    private readonly CityInfoContext _context;

    public CityInforepository(CityInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointsOfInterest)
    {
        var city = await GetCityAsync(cityId,false);
        if(city != null)
        {
            city.PointsOfInterest.Add(pointsOfInterest);
        }
    }

    public async Task<bool> CityExistsAsync(int cityId)
    {
        return await _context.Cities.AnyAsync(c => c.Id == cityId);
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterest)
    {
        _context.PointOfInterest.Remove(pointOfInterest);
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetCitiesAsync(string? name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return await GetCitiesAsync();
        }

        name = name.Trim(); //get rid of whitespaces at start and finish

        return await _context.Cities.Where(c => c.Name == name).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
    {
        if(includePointsOfInterest)
        {
        return await _context.Cities.Include(c => c.PointsOfInterest).Where( c=> c.Id == cityId).FirstOrDefaultAsync();
        }
        return await _context.Cities.Where( c=> c.Id == cityId).FirstOrDefaultAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointsOfInterestId)
    {
        return await _context.PointOfInterest.Where(p => p.cityId == cityId && p.Id == pointsOfInterestId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
    {
        return await _context.PointOfInterest.Where(p => p.cityId == cityId).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()>=0);
    }
}