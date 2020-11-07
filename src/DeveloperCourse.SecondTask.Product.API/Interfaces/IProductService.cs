using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductDto> GetProduct(Guid productId);
    }
}