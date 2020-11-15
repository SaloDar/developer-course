using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Clients.Product.DTOs.Responses
{
    public class GetProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}