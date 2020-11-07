using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Price.API.DTOs;
using Money;

namespace DeveloperCourse.SecondTask.Price.API.Interfaces
{
    public interface IPriceService
    {
        Task<IEnumerable<PriceDto>> GetAllPrices();

        Task<IEnumerable<PriceDto>> GetProductPrices(Guid productId);

        Task<PriceDto> CreatePrice(Guid productId, decimal retailPrice, decimal costPrice, Currency currency);
    }
}