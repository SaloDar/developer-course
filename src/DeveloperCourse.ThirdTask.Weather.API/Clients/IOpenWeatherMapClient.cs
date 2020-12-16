using System.Threading.Tasks;
using DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Types;
using Refit;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients
{
    public interface IOpenWeatherMapClient
    {
        [Get("/weather")]
        Task<GetCityWeatherResponse> GetCityWeather([AliasAs("q")] string cityName, [AliasAs("units")] Unit unit);  
        
        [Get("/forecast")]
        Task<GetsCityForecast> GetsCityForecast([AliasAs("q")] string cityName, [AliasAs("units")] Unit unit);
    }
}