using System.ComponentModel.DataAnnotations;
using DeveloperCourse.ThirdTask.Weather.API.Types;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs
{
    public class GetCityWindRequest
    {
        /// <summary>
        /// City name.
        /// </summary>
        [Required]
        [FromRoute(Name = "cityName")]
        public string CityName { get; set; }
        
        /// <summary>
        /// Unit of speed
        /// meter/sec, miles/hour.
        /// </summary>
        [FromQuery(Name = "metric")]
        [EnumDataType(typeof(SpeedUnit))]
        public SpeedUnit Metric { get; set; } = SpeedUnit.MeterSec;
    }
}