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

    public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
    {
        return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterest)
    {
        _context.PointOfInterest.Remove(pointOfInterest);
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<(IEnumerable<City>,PaginationMetadata)> GetCitiesAsync(string? name,string? searchQuery,int pageNumber,int pageSize)
    {
        // if(string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(searchQuery))
        // {
        //     return await GetCitiesAsync();
        // }

        // collection to start from
        var collection = _context.Cities as IQueryable<City>;

        if(!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim(); //get rid of whitespaces at start and finish
            collection = collection.Where(c => c.Name == name);
        }

        
        if(!string.IsNullOrWhiteSpace(searchQuery))
        {
            searchQuery = searchQuery.Trim();
            collection = collection.Where(a => a.Name.Contains(searchQuery) || (a.Description != null && a.Description.Contains(searchQuery)));
        }

        var TotalItemCount = await collection.CountAsync();
        
        var PaginationMetadata = new PaginationMetadata(TotalItemCount,pageSize,pageNumber);

         var CollectionToReturn = await collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

         return (CollectionToReturn,PaginationMetadata);

        //return await _context.Cities.Where(c => c.Name == name).OrderBy(c => c.Name).ToListAsync();
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