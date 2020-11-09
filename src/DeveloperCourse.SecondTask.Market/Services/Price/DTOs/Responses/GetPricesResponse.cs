using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.Price.DTOs.Responses
{
    public class GetPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}