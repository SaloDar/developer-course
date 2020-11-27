using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Product.DTOs.Responses
{
    public class GetProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}