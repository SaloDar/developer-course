using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Market.Services.Price.DTOs.Requests;
using DeveloperCourse.SecondTask.Market.Services.Price.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondTask.Market.Services.Price
{
    public interface IPriceService
    {
        [Post("/prices")]
        Task<CreatePriceResponse> CreatePrice([Body] CreatePriceRequest request);

        [Get("/prices/{id}")]
        Task<GetPriceReponse> GetPrice(Guid id);

        [Get("/prices")]
        Task<GetPricesResponse> GetPrices(Guid? productId = null, bool? lasted = null);

        [Patch("/prices/{id}")]
        Task<UpdatePriceResponse> UpdatePrice(Guid id, [Body] UpdatePriceRequest request);

        [Delete("/prices/{id}")]
        Task DeletePrice(Guid id);
    }
}