using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class ForecastDayMain
    {
        /// <summary>
        /// Temperature.
        /// </summary>
        [JsonProperty("temp")]
        public float Temperature { get; set; }
    }
}