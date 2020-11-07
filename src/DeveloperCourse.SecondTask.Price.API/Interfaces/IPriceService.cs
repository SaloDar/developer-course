using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Price.API.DTOs;

namespace DeveloperCourse.SecondTask.Price.API.Interfaces
{
    public interface IPriceService
    {
        Task<IEnumerable<PriceDto>> GetAllPrices();

        Task<IEnumerable<PriceDto>> GetProductPrices(Guid productId);
    }
}