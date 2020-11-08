using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IPriceService
    {
        [Get("/prices")]
        Task<GetPricesDto> GetPrices(Guid? productId = null, bool? lasted = null);  
        
        [Get("/prices/{id}")]
        Task<GetPriceDto> GetPrice(Guid id);
    }
}