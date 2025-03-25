using CityApi.Model;

namespace CityApi.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<City?> GetCityByIdAsync(int id);
    Task<City> AddCityAsync(City city);
    Task<City> UpdateCityAsync(City city);
    Task DeleteCityAsync(City city);
}