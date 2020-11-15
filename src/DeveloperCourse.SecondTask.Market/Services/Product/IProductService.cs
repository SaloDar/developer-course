using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Market.Services.Product.DTOs.Requests;
using DeveloperCourse.SecondTask.Market.Services.Product.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondTask.Market.Services.Product
{
    public interface IProductService
    {
        [Get("/products")]
        Task<CreateProductResponse> CreateProduct([Body] CreateProductRequest request);

        [Get("/products")]
        Task<GetProductsResponse> GetProducts();

        [Get("/products/{productId}")]
        Task<GetProductResponse> GetProduct(Guid productId);

        [Put("/products/{productId}")]
        Task<UpdateProductResponse> UpdateProduct(Guid productId, [Body] UpdateProductRequest request);

        [Delete("/products/{productId}")]
        Task DeleteProduct(Guid productId);
    }
}