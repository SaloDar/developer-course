using System.Threading.Tasks;
using DeveloperCourse.ThirdTask.Weather.API.Services.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Types;

namespace DeveloperCourse.ThirdTask.Weather.API.Interfaces
{
    public interface IWeatherService
    {
        Task<CityTemperature> GetCityTemperature(string cityName, TemperatureUnit unit);

        Task<CityWind> GetCityWind(string cityName, SpeedUnit unit);

        Task<CityForecast> GetCityForecast(string cityName, TemperatureUnit unit);
    }
}