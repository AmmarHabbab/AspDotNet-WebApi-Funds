using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;
using WebApi1.Services;

namespace WebApi1.Controllers{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
public class PointsOfInterestController : ControllerBase
{
  // this is called constracter injection which is the preffered way of requesting dependencies
  private readonly ILogger<PointsOfInterestController> _logger;
  private readonly IMailService _mailService;
  private readonly ICityInfoRepository _cityInfoRepository;
  private readonly IMapper _mapper;

        // private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,IMailService mailService,ICityInfoRepository cityInfoRepository, IMapper mapper)//CitiesDataStore citiesDataStore,
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            // _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
            // for cases that is not feasable u can request a service from the container directly HttpContext.RequestServices.GetService() however its advised to use costructer injection when possible

        }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
    {
      // try
      // {
      //  // throw new Exception("Exception Sample.");
      //   var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

      //   if(city == null)
      //   {
      //     _logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest");
      //       return NotFound();
      //   }

      //   return Ok(city.PointsOfInterest);
      // }
      // catch (Exception ex) //System.Exception
      // {
      //   _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.",ex);
      //   return StatusCode(500,"A Problem happened while hanlding your request!");
      //  // throw;
      // }
      if(!await _cityInfoRepository.CityExistsAsync(cityId))
      {
        _logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest");
        return NotFound();
      }

      var PointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

      return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(PointsOfInterestForCity));
        
    }

    [HttpGet("{pointofinterestid}",Name = "GetPointOfInterest")]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId, int pointofinterestid)
    {
//         var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

//         if(city == null)
//         {
//             return NotFound();
//         }

// var PointOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointofinterestid);

// if(PointOfInterest == null)
//  {
//             return NotFound();
//         }
//         return Ok(PointOfInterest);

if(!await _cityInfoRepository.CityExistsAsync(cityId))
      {
        return NotFound();
      }

      var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointofinterestid);

      if(pointOfInterest == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
    }

    [HttpPost]
     public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
    {
        // if(!ModelState.IsValid)
        // {
        //     return BadRequest();
        // }

        //   var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //   if(city == null)
        // {
        //     return NotFound();
        // }

        // var maxPointOfInterestId =_citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p=> p.Id);

        // var finalPointOfInterest = new PointOfInterestDto()
        // {
        //     Id = ++maxPointOfInterestId,
        //     Name = pointOfInterest.Name,
        //     Description = pointOfInterest.Description
        // };

        // city.PointsOfInterest.Add(finalPointOfInterest);

        // return CreatedAtRoute("GetPointOfInterest",new {
        //     cityId = cityId,
        //     pointOfInterestid = finalPointOfInterest.Id,
        // },
        // finalPointOfInterest);

        if(!await _cityInfoRepository.CityExistsAsync(cityId))
        {
          return NotFound();
        }

        var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

        await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId,finalPointOfInterest);

        await _cityInfoRepository.SaveChangesAsync();

        var CreatedPointOfInterestToReturn =  _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

        return CreatedAtRoute("GetPointOfInterest",new {
            cityId = cityId,
            pointOfInterestid = CreatedPointOfInterestToReturn.Id,
        },
        CreatedPointOfInterestToReturn);
    }

    [HttpPut("{pointOfInterestId}")]
     public async Task<ActionResult> UpdatePointOfInterest(int cityId,int pointOfInterestId,PointOfInterestForUpdateDto pointOfInterest)
    {
        // if(!ModelState.IsValid)
        // {
        //     return BadRequest();
        // }

        //   var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //   if(city == null)
        // {
        //     return NotFound();
        // }

        //   var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //   if(pointOfInterestFromStore == null)
        //   {
        //     return NotFound();
        //   }

        //   pointOfInterestFromStore.Name = pointOfInterest.Name;
        //   pointOfInterestFromStore.Description = pointOfInterest.Description;

        //   return NoContent();

 if(!await _cityInfoRepository.CityExistsAsync(cityId))
        {
          return NotFound();
        }

        var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointOfInterestId);
        if(pointOfInterestEntity == null)
        {
          return NotFound();
        }

        _mapper.Map(pointOfInterest,pointOfInterestEntity);

        await _cityInfoRepository.SaveChangesAsync();

        return NoContent();      
    }

    [HttpPatch("{pointofinterestid}")]
    public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId,int pointOfInterestId,JsonPatchDocument <PointOfInterestForUpdateDto> patchDocument)
    {
      //  var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
      //     if(city == null)
      //   {
      //       return NotFound();
      //   }

      //   var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

      //     if(pointOfInterestFromStore == null)
      //     {
      //       return NotFound();
      //     }

      //      var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
      //      {
      //       Name = pointOfInterestFromStore.Name,
      //       Description = pointOfInterestFromStore.Description
      //      };

      //     patchDocument.ApplyTo(pointOfInterestToPatch,ModelState);

      //     if(!ModelState.IsValid)
      //     {
      //       return BadRequest();
      //     }

      //     if(!TryValidateModel(pointOfInterestToPatch))
      //     {
      //       return BadRequest(ModelState);
      //     }

      //     pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
      //     pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

      //     return NoContent();

      if(!await _cityInfoRepository.CityExistsAsync(cityId))
      {
        return NotFound();
      }

      var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointOfInterestId);
      if(pointOfInterestEntity == null)
      {
        return NotFound();
      }

      var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

      patchDocument.ApplyTo(pointOfInterestToPatch,ModelState);

           if(!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }

          if(!TryValidateModel(pointOfInterestToPatch))
          {
            return BadRequest(ModelState);
          }

          _mapper.Map(pointOfInterestToPatch,pointOfInterestEntity);

          await _cityInfoRepository.SaveChangesAsync();

          return NoContent();

    }

//     [HttpDelete("{pointofinterestid}")]
//     public ActionResult DeletePointOfInterest(int cityId,int pointOfInterestId)
//     {
//        var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
//           if(city == null)
//         {
//             return NotFound();
//         }

//         var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

//           if(pointOfInterestFromStore == null)
//           {
//             return NotFound();
//           }

//           city.PointsOfInterest.Remove(pointOfInterestFromStore);
//           _mailService.Send("Point of Interest Deleted.",
//           $"Point Of Interest {pointOfInterestFromStore.Name} with Id {pointOfInterestFromStore.Id} was deleted!");
//           return NoContent();
// }

}
}