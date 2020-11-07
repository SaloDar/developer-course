using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetAllProducts
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}