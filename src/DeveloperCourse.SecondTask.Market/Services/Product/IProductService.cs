using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.Product.DTOs.Responses;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services.Product
{
    public interface IProductService
    {
        [Get("/products")]
        Task<GetProductsResponse> GetProducts();

        [Get("/products/{productId}")]
        Task<GetProductResponse> GetProduct(Guid productId);
    }
}