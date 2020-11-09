using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.Product.DTOs.Responses
{
    public class GetProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}