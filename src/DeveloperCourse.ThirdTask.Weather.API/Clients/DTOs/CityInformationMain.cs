using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class CityInformationMain
    {
        /// <summary>
        /// Temperature.
        /// </summary>
        [JsonProperty("temp")]
        public float Temperature { get; set; }
    }
}