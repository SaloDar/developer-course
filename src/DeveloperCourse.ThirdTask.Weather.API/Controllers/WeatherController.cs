using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs;
using DeveloperCourse.ThirdTask.Weather.API.Infrastructure.Attributes;
using DeveloperCourse.ThirdTask.Weather.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.ThirdTask.Weather.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController
    {
        private readonly IMapper _mapper;

        private readonly IWeatherService _weatherService;

        public WeatherController(IMapper mapper, IWeatherService weatherService)
        {
            _mapper = mapper;
            _weatherService = weatherService;
        }
        
        /// <summary>
        /// Get temperature information by city name or state code and country code.
        /// </summary>
        /// <returns>Returns temperature information</returns>
        /// <response code="200">Returns temperature information</response>
        [HttpGet("{cityName}/temperature")]
        [ProducesResponseType(typeof(GetCityTemperatureResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetCityTemperatureResponse> GetCityTemperature([FromMultiSource] GetCityTemperatureRequest request)
        {
            var result = await _weatherService.GetCityTemperature(request.CityName, request.Metric);

            return _mapper.Map<GetCityTemperatureResponse>(result);
        }

        /// <summary>
        /// Get wind information by city name or state code and country code.
        /// </summary>
        /// <returns>Returns wind information</returns>
        /// <response code="200">Returns wind information</response>
        [HttpGet("{cityName}/wind")]
        [ProducesResponseType(typeof(GetCityWindResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetCityWindResponse> GetCityWind([FromMultiSource] GetCityWindRequest request)
        {
            var result = await _weatherService.GetCityWind(request.CityName, request.Metric);

            return _mapper.Map<GetCityWindResponse>(result);
        }
        
        /// <summary>
        /// Get weather forecast for 5 days with data every 3 hours by city name or state code and country code.
        /// </summary>
        /// <returns>Returns weather forecast</returns>
        /// <response code="200">Returns weather forecast</response>
        [HttpGet("{cityName}/future")]
        [ProducesResponseType(typeof(GetCityForecastResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetCityForecastResponse> GetCityForecast([FromMultiSource] GetCityForecastRequest request)
        {
            var result = await _weatherService.GetCityForecast(request.CityName, request.Metric);
            
            return _mapper.Map<GetCityForecastResponse>(result);
        }
    }
}