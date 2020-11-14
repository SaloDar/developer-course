using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> CreateProduct(string name, string description, string sku, string weight);

        Task<ProductDto> GetProduct(Guid productId);

        Task DeleteProduct(Guid id);

        Task<ProductDto> UpdateProduct(Guid id, string name, string description, string sku, string weight);
    }
}