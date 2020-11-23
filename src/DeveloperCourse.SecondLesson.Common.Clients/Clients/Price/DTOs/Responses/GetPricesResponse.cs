using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Price.DTOs.Responses
{
    public class GetPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}