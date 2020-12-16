using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class GetCityWeatherResponse
    {
        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Main information.
        /// </summary>
        [JsonProperty("main")]
        public CityInformationMain Main { get; set; }

        /// <summary>
        /// Wind information.
        /// </summary>
        [JsonProperty("wind")]
        public CityInformationWind Wind { get; set; }
    }
}