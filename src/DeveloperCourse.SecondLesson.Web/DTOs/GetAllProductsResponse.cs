using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.DTOs
{
    public class GetAllProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}