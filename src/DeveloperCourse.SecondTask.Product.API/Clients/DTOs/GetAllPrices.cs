using System.Collections.Generic;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Clients.DTOs
{
    public class GetAllPrices
    {
        public IEnumerable<PriceDto> Prices { get; set; }
    }
}