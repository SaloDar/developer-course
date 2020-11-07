using System.Collections.Generic;
using DeveloperCourse.SecondTask.Price.API.DTOs;

namespace DeveloperCourse.SecondTask.Price.API.Controllers.DTOs
{
    public class GetAllPricesResponse
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}