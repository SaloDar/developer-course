using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class CityInformationWind
    {
        /// <summary>
        /// Wind speed.
        /// Unit Default: meter/sec,
        /// Metric: meter/sec,
        /// Imperial: miles/hour.
        /// </summary>
        [JsonProperty("speed")]
        public float Speed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological).
        /// </summary>
        [JsonProperty("deg")]
        public uint Degrees { get; set; }
    }
}