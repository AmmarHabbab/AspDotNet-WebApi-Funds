using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;

namespace WebApi1.Controllers{

[ApiController]
[Route("api/cities")] //api/[controller]
public class CitiesController : ControllerBase
{
   // [HttpGet("api/cities")]
    public ActionResult<IEnumerable<CityDto>> GetCities() //public JsonResult GetCities()
    {
        // return new JsonResult(new List<object>{
        //     new {id=1,Name="New York City"},
        //     new {id=2,Name="Antwerp"},
        // });

      //  return new JsonResult(CitiesDataStore.Current.Cities);

      return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id) // public JsonResult GetCity(int id)
    {
        // return new JsonResult(new List<object>{
        //     new {id=1,Name="New York City"},
        //     new {id=2,Name="Antwerp"},
        // });

       // return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));

       var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

       if(cityToReturn == null)
       {
        return NotFound();
       }

       return Ok(cityToReturn);
    }
}

}