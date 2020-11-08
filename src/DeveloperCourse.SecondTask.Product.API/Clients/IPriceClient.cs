using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondTask.Product.API.Clients
{
    public interface IPriceClient
    {
        [Get("/prices")]
        Task<GetPricesDto> GetPrices(Guid? productId = null, bool? lasted = null);  
        
        [Get("/prices/{id}")]
        Task<GetPriceDto> GetPrice(Guid id);
    }
}