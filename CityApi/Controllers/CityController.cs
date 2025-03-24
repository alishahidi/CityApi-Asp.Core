using CityApi.Model;
using CityApi.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityApi.Controllers;

[ApiController]
[Route("/api/Cities")]
public class CityController(CityService cityService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<City>> LoadAll()
    {
        return Ok(cityService.GetCities());
    }

    [HttpGet("{id}")]
    public ActionResult<City> LoadById(int id)
    {
        var city = cityService.FindById(id);

        if (city == null)
        {
            return NotFound();
        }
        
        return Ok(city);
    }

    [HttpPatch("{id}")]
    public ActionResult<City> Update(int id, [FromBody] JsonPatchDocument<City> patch)
    {
        if (patch == null)
        {
            return BadRequest();
        }
        
        var city = cityService.FindById(id);

        if (city == null)
        {
            return NotFound();
        }
        
        patch.ApplyTo(city, ModelState);

        if (!TryValidateModel(city))
        {
            return BadRequest(ModelState);
        }
        
        return Ok(cityService.Update(city));
    }

    [HttpPost]
    public ActionResult<City> Add([FromBody] City city)
    {
        return cityService.Add(city);
    }

    [HttpDelete("{id}")]
    public ActionResult<City> Delete(int id)
    {
        cityService.Delete(id);

        return Ok();
    }
}