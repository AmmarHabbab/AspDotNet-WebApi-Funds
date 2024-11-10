using WebApi1.Models;

namespace WebApi1;

public class CitiesDataStore
{
    public List<CityDto> Cities {get;set;}

    public static CitiesDataStore Current {get;} = new CitiesDataStore();


    public CitiesDataStore()
    {
        Cities = new List<CityDto>(){
            new CityDto()
            {
                Id = 1,
                Name = "New York City",
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Central Park",
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Times Square",
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf"
                    }
                }
            },
            new CityDto()
            {
                Id = 2,
                Name = "Antwerp",
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        Name = "sadf",
                        Description = "ddfsgfd gdfsdf"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Times fghgfdhf",
                        Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agfsad fsda dsaf asdf sddf"
                    }
                }
            },
            new CityDto()
            {
                Id = 3,
                Name = "Paris",
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 5,
                        Name = "Censadsa sadtral Park",
                        Description = "dsaf dsaf dsaf sdafsdaf sadsadf sad fsadga agdf"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        Name = "Timxzczxxces Square",
                        Description = "dsaf dsaf zxvxcx v dsaf sdafsdaf sad fsadga agdf"
                    }
                }
            },
        };
    }

}
