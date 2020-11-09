using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.Price.DTOs.Requests;
using DeveloperCourse.ThirdLesson.View.Services.Price.DTOs.Responses;
using Money;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services.Price
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