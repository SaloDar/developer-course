using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetProductPrices
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}