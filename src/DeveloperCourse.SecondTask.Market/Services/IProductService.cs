using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IProductService
    {
        [Get("/product")]
        Task<GetAllProducts> GetAllProducts();

        [Get("/product/{productId}")]
        Task<GetProduct> GetProduct(Guid productId);
    }
}