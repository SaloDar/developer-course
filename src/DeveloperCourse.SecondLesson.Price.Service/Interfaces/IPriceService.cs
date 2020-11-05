using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Price.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Price.Service.Interfaces
{
    public interface IPriceService
    {
        Task<IEnumerable<PriceDto>> GetAllPrices();

        Task<IEnumerable<PriceDto>> GetProductPrices(Guid productId);
    }
}