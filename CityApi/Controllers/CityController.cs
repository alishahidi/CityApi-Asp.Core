using AutoMapper;
using CityApi.Dto;
using CityApi.Model;
using CityApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityApi.Controllers;

[ApiController]
[Route("/api/Cities")]
public class CityController(
    ICityRepository _cityRepository,
    IMapper _mapper
    ) : ControllerBase
{
    // Get all cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDto>>> LoadAll()
    {
        var cities = await _cityRepository.GetCitiesAsync();
        return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
    }

    // Get city by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> LoadById(int id)
    {
        var city = await _cityRepository.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return Ok(city);
    }

    // Partially update a city using JSON Patch
    [HttpPatch("{id}")]
    public async Task<ActionResult<City>> Update(int id, [FromBody] JsonPatchDocument<City> patch)
    {
        if (patch == null)
        {
            return BadRequest();
        }

        var city = await _cityRepository.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        patch.ApplyTo(city, ModelState);

        if (!TryValidateModel(city))
        {
            return BadRequest(ModelState);
        }

        return Ok(await _cityRepository.UpdateCityAsync(city));
    }

    // Add a new city
    [HttpPost]
    public async Task<ActionResult<City>> Add([FromBody] City city)
    {
        var addedCity = await _cityRepository.AddCityAsync(city);
        return CreatedAtAction(nameof(LoadById), new { id = addedCity.Id }, addedCity);
    }

    // Delete a city
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var city = await _cityRepository.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        await _cityRepository.DeleteCityAsync(city);
        return NoContent();
    }
}