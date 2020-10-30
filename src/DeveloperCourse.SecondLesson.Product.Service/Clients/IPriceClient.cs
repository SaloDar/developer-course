using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Product.Service.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondLesson.Product.Service.Clients
{
    public interface IPriceClient
    {
        [Get("/price")]
        Task<GetAllPrices> GetAllPrices();

        [Get("/price/product/{productId}")]
        Task<GetProductPrices> GetProductImages(Guid productId);
    }
}