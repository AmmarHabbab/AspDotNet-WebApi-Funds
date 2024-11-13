using AutoMapper;

namespace WebApi1.Profiles;

public class PointOfInterestProfile : Profile
{
    public PointOfInterestProfile()
    {
        CreateMap<Entities.PointOfInterest,Models.PointOfInterestDto>();
        CreateMap<Models.PointOfInterestForCreationDto,Entities.PointOfInterest>();
    }
}