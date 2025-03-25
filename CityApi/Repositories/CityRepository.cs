using CityApi.Context;
using CityApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Repositories;

public class CityRepository(CityContext _cityContext) : ICityRepository
{
    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _cityContext.Cities
            .OrderBy(city => city.Name) // Optional ordering, can be customized
            .ToListAsync();
    }

    public async Task<City?> GetCityByIdAsync(int id)
    {
        return await _cityContext.Cities
            .FirstOrDefaultAsync(city => city.Id == id);
    }

    public async Task<City> AddCityAsync(City city)
    {
        await _cityContext.Cities.AddAsync(city);
        await _cityContext.SaveChangesAsync();
        return city;
    }

    public async Task<City> UpdateCityAsync(City city)
    {
        _cityContext.Cities.Update(city);
        await _cityContext.SaveChangesAsync();
        return city;
    }

    public async Task DeleteCityAsync(City city)
    {
        _cityContext.Cities.Remove(city);
        await _cityContext.SaveChangesAsync();
    }
}