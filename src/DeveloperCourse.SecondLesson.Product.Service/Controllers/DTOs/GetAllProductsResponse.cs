using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Product.Service.Controllers.DTOs
{
    public class GetAllProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}