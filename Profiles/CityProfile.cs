using AutoMapper;

namespace WebApi1.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<Entities.City,Models.CityWithoutPointOfInterestDto>();
    }
}