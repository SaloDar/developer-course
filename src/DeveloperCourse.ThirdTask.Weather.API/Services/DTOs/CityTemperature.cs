using DeveloperCourse.ThirdTask.Weather.API.Types;

namespace DeveloperCourse.ThirdTask.Weather.API.Services.DTOs
{
    public class CityTemperature
    {
        
        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Temperature.
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Unit of temperature.
        /// Kelvin, Celsius, Fahrenheit.
        /// </summary>
        public TemperatureUnit Metric { get; set; }
    }
}