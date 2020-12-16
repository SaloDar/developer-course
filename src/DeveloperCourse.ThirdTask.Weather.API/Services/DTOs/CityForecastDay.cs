using System;
using DeveloperCourse.ThirdTask.Weather.API.Types;

namespace DeveloperCourse.ThirdTask.Weather.API.Services.DTOs
{
    public class CityForecastDay
    {
        /// <summary>
        /// Time of data forecasted.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Temperature.
        /// Unit Default: Kelvin,
        /// Metric: Celsius,
        /// Imperial: Fahrenheit.
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Unit.
        /// Standard, Metric, Imperial.
        /// </summary>
        public TemperatureUnit Metric { get; set; }
    }
}