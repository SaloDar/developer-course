using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondTask.Product.API.Clients
{
    public interface IPriceClient
    {
        [Get("/price")]
        Task<GetAllPrices> GetAllPrices();

        [Get("/price/product/{productId}")]
        Task<GetProductPrices> GetProductImages(Guid productId);
    }
}