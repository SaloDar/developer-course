using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Market.Services.Product.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondTask.Market.Services.Product
{
    public interface IProductService
    {
        [Get("/products")]
        Task<GetProductsResponse> GetProducts();

        [Get("/products/{productId}")]
        Task<GetProductResponse> GetProduct(Guid productId);
    }
}