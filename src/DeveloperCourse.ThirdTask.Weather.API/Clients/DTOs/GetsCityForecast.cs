using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class GetsCityForecast
    {
        /// <summary>
        /// List of days.
        /// </summary>
        [JsonProperty("list")]
        public ICollection<ForecastDay> Days { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        [JsonProperty("city")]
        public ForecastCity City { get; set; }
    }
}