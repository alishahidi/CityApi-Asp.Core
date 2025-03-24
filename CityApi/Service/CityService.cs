using CityApi.Context;
using CityApi.Model;

namespace CityApi.Service;

public class CityService(CityContext context, ILogger<CityService> logger)
{
    public IEnumerable<City> GetCities()
    {
        return context.Cities;
    }
    
    public City FindById(int id)
    {
        logger.LogInformation($"Getting city with id: {id}");
        return (from city in context.Cities
            where city.Id == id
                select city).FirstOrDefault();
    }
    
    public City Add(City city)
    {
        logger.LogInformation($"Adding city: {city}");
        context.Cities.Add(city);

        return city;
    }
    
    public void Delete(int id)
    {
        logger.LogInformation($"Deleting city with id: {id}");
        context.Cities.RemoveAll(c => c.Id == id);
    }
    
    public City Update(City city)
    {
        logger.LogInformation($"Updating city: {city}");
        var existingCity = FindById(city.Id);
        if (existingCity == null)
        {
            return null;
        }

        // Update the existing city with the new values
        existingCity.Name = city.Name;
        existingCity.Province = city.Province;

        return existingCity;
    }
}