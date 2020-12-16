using System;

namespace DeveloperCourse.ThirdTask.Weather.API.Infrastructure.Configs
{
    public class WeatherConfig
    {
        /// <summary>
        /// Link to API.
        /// </summary>
        public Uri ApiLink { get; set; }

        /// <summary>
        /// API key.
        /// </summary>
        public string Token { get; set; }
    }
}