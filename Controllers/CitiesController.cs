using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;
using WebApi1.Services;

namespace WebApi1.Controllers{

[ApiController]
[Route("api/cities")] //api/[controller]
public class CitiesController : ControllerBase
{
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20; 
        public CitiesController(ICityInfoRepository cityInfoRepository,IMapper mapper)
    {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        

        // private readonly CitiesDataStore _citiesDataStore;
        // public CitiesController(CitiesDataStore citiesDataStore)
        // {
        //   _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        // }

        // [HttpGet("api/cities")]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> 
        GetCities([FromQuery] string? name,[FromQuery] string? searchQuery,[FromQuery] int pageNumber=1,[FromQuery] int pageSize = 10) // not neccessirly [fromquery] u can also assign name property on from query attribute [FromQuery(Name = "filterorname")]
         //public JsonResult GetCities()
    {
        // return new JsonResult(new List<object>{
        //     new {id=1,Name="New York City"},
        //     new {id=2,Name="Antwerp"},
        // });

      //  return new JsonResult(CitiesDataStore.Current.Cities);

      //return Ok(_citiesDataStore.Cities);
      if(pageSize > maxCitiesPageSize)
      {
        pageSize = maxCitiesPageSize;
      }

      var (cityEntites,paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name,searchQuery,pageNumber,pageSize);

      // var results = new List<CityWithoutPointOfInterestDto>();
      // foreach(var cityEntity in cityEntites)
      // {
      //   results.Add(new CityWithoutPointOfInterestDto{
      //     Id = cityEntity.Id,
      //     Name = cityEntity.Name,
      //     Description = cityEntity.Description
      //   });
      // }

     // return Ok(results);

     Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));

     return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntites));// automapper is much better than mapping ourselves like above
    }

   [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id,bool includePointsOfInterest = false) // public JsonResult GetCity(int id)
    {
        // return new JsonResult(new List<object>{
        //     new {id=1,Name="New York City"},
        //     new {id=2,Name="Antwerp"},
        // });

       // return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));

      //  var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

      //  if(cityToReturn == null)
      //  {
      //   return NotFound();
      //  }

      // return Ok(cityToReturn);

      var city = await _cityInfoRepository.GetCityAsync(id,includePointsOfInterest);
      if(city == null)
      {
        return NotFound();
      }

      if(includePointsOfInterest)
      {
        return Ok(_mapper.Map<CityDto>(city));
      }
      return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
    }
 }

}