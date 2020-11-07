using System.Collections.Generic;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Clients.DTOs
{
    public class GetProductPrices
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}