using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetAllPrices
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}