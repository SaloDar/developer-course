using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Price.DTOs.Requests;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Price.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Price
{
    [Headers("Authorization: Bearer")]
    public interface IPriceClient
    {
        [Post("/prices")]
        Task<CreatePriceResponse> CreatePrice([Body] CreatePriceRequest request);

        [Get("/prices/{id}")]
        Task<GetPriceReponse> GetPrice(Guid id);

        [Get("/prices")]
        Task<GetPricesResponse> GetPrices();   
        
        [Get("/prices")]
        Task<GetPricesResponse> GetPrices(Guid productId);   
        
        [Get("/prices")]
        Task<GetPricesResponse> GetPrices(bool lasted); 
        
        [Get("/prices")]
        Task<GetPricesResponse> GetPrices(Guid productId , bool lasted);

        [Patch("/prices/{id}")]
        Task<UpdatePriceResponse> UpdatePrice(Guid id, [Body] UpdatePriceRequest request);

        [Delete("/prices/{id}")]
        Task DeletePrice(Guid id);
    }
}