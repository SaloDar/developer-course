using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetPricesDto
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}