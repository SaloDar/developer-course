using System.Collections.Generic;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class GetAllProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}