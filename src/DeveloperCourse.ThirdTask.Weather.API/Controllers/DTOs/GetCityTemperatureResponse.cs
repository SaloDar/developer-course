namespace DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs
{
    public class GetCityTemperatureResponse
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
        public string Metric { get; set; }
    }
}