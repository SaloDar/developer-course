using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Services.Product.DTOs.Responses
{
    public class GetProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}