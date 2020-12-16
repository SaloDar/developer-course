using System.ComponentModel.DataAnnotations;
using DeveloperCourse.ThirdTask.Weather.API.Types;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs
{
    public class GetCityTemperatureRequest
    {
        /// <summary>
        /// City name.
        /// </summary>
        [Required]
        [FromRoute(Name = "cityName")]
        public string CityName { get; set; }

        /// <summary>
        /// Unit of temperature
        /// Kelvin, Celsius, Fahrenheit.
        /// </summary>
        [FromQuery(Name = "metric")]
        [EnumDataType(typeof(TemperatureUnit))]
        public TemperatureUnit Metric { get; set; } = TemperatureUnit.Celsius;
    }
}