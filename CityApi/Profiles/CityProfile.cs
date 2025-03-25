using AutoMapper;
using CityApi.Dto;
using CityApi.Model;

namespace CityApi.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
    }
    
}