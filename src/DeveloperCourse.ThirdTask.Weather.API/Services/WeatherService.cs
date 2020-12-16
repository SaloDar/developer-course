using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.ThirdTask.Weather.API.Clients;
using DeveloperCourse.ThirdTask.Weather.API.Interfaces;
using DeveloperCourse.ThirdTask.Weather.API.Services.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Types;

namespace DeveloperCourse.ThirdTask.Weather.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapClient _openWeatherMapClient;

        private readonly IMapper _mapper;

        public WeatherService(IOpenWeatherMapClient openWeatherMapClient, IMapper mapper)
        {
            _openWeatherMapClient = openWeatherMapClient;
            _mapper = mapper;
        }

        public async Task<CityTemperature> GetCityTemperature(string cityName, TemperatureUnit temperatureUnit)
        {
            var unit = _mapper.Map<Unit>(temperatureUnit);

            var cityInfo = await _openWeatherMapClient.GetCityWeather(cityName, unit);

            return new CityTemperature
            {
                City = cityInfo.Name, Temperature = cityInfo.Main.Temperature, Metric = temperatureUnit
            };
        }

        public async Task<CityWind> GetCityWind(string cityName, SpeedUnit speedUnit)
        {
            var unit = _mapper.Map<Unit>(speedUnit);

            var cityInfo = await _openWeatherMapClient.GetCityWeather(cityName, unit);

            var windDirection = cityInfo.Wind.Degrees switch
            {
                < 45 or >= 360   => CardinalDirection.North,
                >= 45 and < 90   => CardinalDirection.Northeast,
                >= 90 and < 135  => CardinalDirection.East,
                >= 135 and < 180 => CardinalDirection.Southeast,
                >= 180 and < 225 => CardinalDirection.South,
                >= 225 and < 270 => CardinalDirection.Southwest,
                >= 270 and < 315 => CardinalDirection.West,
                >= 315 and < 360 => CardinalDirection.North
            };

            return new CityWind
            {
                City = cityInfo.Name,
                Speed = cityInfo.Wind.Speed,
                Direction = windDirection,
                DirectionDegrees = cityInfo.Wind.Degrees,
                Metric = speedUnit, 
            };
        }

        public async Task<CityForecast> GetCityForecast(string cityName, TemperatureUnit temperatureUnit)
        {
            var unit = _mapper.Map<Unit>(temperatureUnit);

            var cityInfo = await _openWeatherMapClient.GetsCityForecast(cityName, unit);

            return new CityForecast
            {
                Days = cityInfo.Days.Select(x => new CityForecastDay
                    {
                        City = cityInfo.City.Name,
                        Temperature = x.Main.Temperature,
                        Metric = temperatureUnit,
                        Date = x.Date
                    })
                    .ToList()
            };
        }
    }
}