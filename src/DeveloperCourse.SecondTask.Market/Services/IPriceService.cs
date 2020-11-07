using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IPriceService
    {
        [Get("/price")]
        Task<GetAllPrices> GetAllPrices();

        [Get("/price/product/{productId}")]
        Task<GetProductPrices> GetProductImages(Guid productId);
    }
}