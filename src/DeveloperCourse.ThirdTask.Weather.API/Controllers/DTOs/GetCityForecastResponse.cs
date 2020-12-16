using System.Collections.Generic;

namespace DeveloperCourse.ThirdTask.Weather.API.Controllers.DTOs
{
    public class GetCityForecastResponse
    {
        /// <summary>
        /// Weather forecast days.
        /// </summary>
        public ICollection<CityForecastDay> Days { get; set; }
    }
}