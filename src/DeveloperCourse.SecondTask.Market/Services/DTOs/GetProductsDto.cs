using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetProductsDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}