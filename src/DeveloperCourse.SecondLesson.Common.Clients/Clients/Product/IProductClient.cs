using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Product.DTOs.Requests;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Product.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Product
{
    [Headers("Authorization: Bearer")]
    public interface IProductClient
    {
        [Post("/products")]
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