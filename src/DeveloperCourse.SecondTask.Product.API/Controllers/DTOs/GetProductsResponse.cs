using System.Collections.Generic;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class GetProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}