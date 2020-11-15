using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Clients.Price.DTOs.Responses
{
    public class GetPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}