using Microsoft.AspNetCore.Mvc;
using WebApi1.Models;

namespace WebApi1.Controllers{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

        if(city == null)
        {
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
}


}