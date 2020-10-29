using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Product.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductDto> GetProduct(Guid productId);
    }
}