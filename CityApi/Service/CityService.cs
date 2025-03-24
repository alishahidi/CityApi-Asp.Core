using CityApi.Context;
using CityApi.Model;

namespace CityApi.Service;

public class CityService(CityContext context)
{
    public IEnumerable<City> GetCities()
    {
        return context.Cities;
    }
    
    public City FindById(int id)
    {
        return (from city in context.Cities
            where city.Id == id
                select city).FirstOrDefault();
    }
    
    public City Add(City city)
    {
        context.Cities.Add(city);

        return city;
    }
    
    public void Delete(int id)
    {
        context.Cities.RemoveAll(c => c.Id == id);
    }
    
    public City Update(City city)
    {
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