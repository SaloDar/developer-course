using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Product.Service.Clients.DTOs
{
    public class GetProductPrices
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}