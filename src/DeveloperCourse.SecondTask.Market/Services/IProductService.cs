using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IProductService
    {
        [Get("/products")]
        Task<GetProductsDto> GetProducts();

        [Get("/products/{productId}")]
        Task<GetProductDto> GetProduct(Guid productId);
    }
}