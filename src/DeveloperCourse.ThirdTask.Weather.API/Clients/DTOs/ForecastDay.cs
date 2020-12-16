using System;
using Newtonsoft.Json;

namespace DeveloperCourse.ThirdTask.Weather.API.Clients.DTOs
{
    public class ForecastDay
    {
        /// <summary>
        /// Main information.
        /// </summary>
        [JsonProperty("main")]
        public ForecastDayMain Main { get; set; }

        /// <summary>
        /// Date.
        /// </summary>
        [JsonProperty("dt_txt")]
        public DateTime Date { get; set; }
    }
}