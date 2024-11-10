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
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf."
            },
            new CityDto()
            {
                Id = 2,
                Name = "Antwerp",
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf."
            },
            new CityDto()
            {
                Id = 3,
                Name = "Paris",
                Description = "dsaf dsaf dsaf sdafsdaf sad fsadga agdf."
            },
        };
    }

}
