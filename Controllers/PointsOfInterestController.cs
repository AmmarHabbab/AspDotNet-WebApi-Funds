using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;

namespace WebApi1.Controllers{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
public class PointsOfInterestController : ControllerBase
{
  // this is called constracter injection which is the preffered way of requesting dependencies
  private readonly ILogger<PointsOfInterestController> _logger;

  public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    // for cases that is not feasable u can request a service from the container directly HttpContext.RequestServices.GetService() however its advised to use costructer injection when possible
    
  }


    [HttpGet]
    public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

        if(city == null)
        {
          _logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest");
            return NotFound();
        }

        return Ok(city.PointsOfInterest);
    }

    [HttpGet("{pointofinterestid}",Name = "GetPointOfInterest")]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointofinterestid)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

        if(city == null)
        {
            return NotFound();
        }

var PointOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointofinterestid);

if(PointOfInterest == null)
 {
            return NotFound();
        }
        return Ok(PointOfInterest);
    }

    [HttpPost]
     public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

          var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if(city == null)
        {
            return NotFound();
        }

        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p=> p.Id);

        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };

        city.PointsOfInterest.Add(finalPointOfInterest);

        return CreatedAtRoute("GetPointOfInterest",new {
            cityId = cityId,
            pointOfInterestid = finalPointOfInterest.Id,
        },
        finalPointOfInterest);
    }

    [HttpPut("{pointOfInterestId}")]
     public ActionResult UpdatePointOfInterest(int cityId,int pointOfInterestId,PointOfInterestForUpdateDto pointOfInterest)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

          var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if(city == null)
        {
            return NotFound();
        }

          var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

          if(pointOfInterestFromStore == null)
          {
            return NotFound();
          }

          pointOfInterestFromStore.Name = pointOfInterest.Name;
          pointOfInterestFromStore.Description = pointOfInterest.Description;

          return NoContent();

      
    }

    [HttpPatch("{pointofinterestid}")]
    public ActionResult PartiallyUpdatePointOfInterest(int cityId,int pointOfInterestId,JsonPatchDocument <PointOfInterestForUpdateDto> patchDocument)
    {
       var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if(city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

          if(pointOfInterestFromStore == null)
          {
            return NotFound();
          }

           var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
           {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
           };

          patchDocument.ApplyTo(pointOfInterestToPatch,ModelState);

          if(!ModelState.IsValid)
          {
            return BadRequest();
          }

          if(!TryValidateModel(pointOfInterestToPatch))
          {
            return BadRequest(ModelState);
          }

          pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
          pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

          return NoContent();

    }

    [HttpDelete("{pointofinterestid}")]
    public ActionResult DeletePointOfInterest(int cityId,int pointOfInterestId)
    {
       var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if(city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

          if(pointOfInterestFromStore == null)
          {
            return NotFound();
          }

          city.PointsOfInterest.Remove(pointOfInterestFromStore);
          return NoContent();
}

}
}