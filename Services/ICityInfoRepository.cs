using System.Drawing;
using WebApi1.Entities;

namespace WebApi1.Services;

public interface ICityInfoRepository
{
    Task<IEnumerable<City>> GetCitiesAsync(); // Iquerayble
    Task<(IEnumerable<City>,PaginationMetadata)> GetCitiesAsync(string? name,string? searchQuery,int pageNumber, int pageSize);
    Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
    Task<bool> CityExistsAsync(int cityId);
    Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

    Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointsOfInterestId);
    Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointsOfInterest);
    void DeletePointOfInterest(PointOfInterest pointOfInterest);
    Task<bool> CityNameMatchesCityId(string? cityName,int cityId);
    Task<bool> SaveChangesAsync();
}