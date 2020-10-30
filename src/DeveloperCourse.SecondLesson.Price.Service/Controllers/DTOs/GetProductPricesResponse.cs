using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Price.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Price.Service.Controllers.DTOs
{
    public class GetProductPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}