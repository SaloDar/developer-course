using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Services.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Types;
using CityForecastDay = DeveloperCourse.ThirdTask.Weather.API.Services.DTOs.CityForecastDay;

namespace DeveloperCourse.ThirdTask.Weather.API.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<CityTemperature, GetCityTemperatureResponse>();

            CreateMap<CityWind, GetCityWindResponse>();

            CreateMap<CityForecast, GetCityForecastResponse>();

            CreateMap<CityForecastDay, Controllers.DTOs.CityForecastDay>();

            CreateMap<TemperatureUnit, Unit>()
                .ConvertUsingEnumMapping(opt => opt
                    .MapValue(TemperatureUnit.Kelvin, Unit.Standard)
                    .MapValue(TemperatureUnit.Celsius, Unit.Metric)
                    .MapValue(TemperatureUnit.Fahrenheit, Unit.Imperial)
                ); 
            
            CreateMap<SpeedUnit, Unit>()
                .ConvertUsingEnumMapping(opt => opt
                    .MapValue(SpeedUnit.MeterSec, Unit.Metric)
                    .MapValue(SpeedUnit.MilesHour, Unit.Imperial)
                );
        }
    }
}