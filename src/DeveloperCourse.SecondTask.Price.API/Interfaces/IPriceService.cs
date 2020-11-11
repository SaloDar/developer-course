using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Price.API.DTOs;
using Money;

namespace DeveloperCourse.SecondTask.Price.API.Interfaces
{
    public interface IPriceService
    {
        Task<PriceDto> GetPrice(Guid id);

        Task<PriceDto> UpdatePrice(Guid id, Guid? productId = null, decimal? retailPrice = null,
            decimal? costPrice = null, Currency? currency = null);

        Task DeletePrice(Guid id);

        Task<IEnumerable<PriceDto>> GetPrices(Guid? productId = null, bool? lasted = null);

        Task<PriceDto> CreatePrice(Guid productId, decimal retailPrice, decimal costPrice, Currency currency);
    }
}