using System.Collections.Generic;

namespace DeveloperCourse.ThirdTask.Weather.API.Services.DTOs
{
    public class CityForecast
    {
        /// <summary>
        /// Weather forecast days.
        /// </summary>
        public ICollection<CityForecastDay> Days { get; set; }
    }
}