using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Services.Price.DTOs.Responses
{
    public class GetPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}