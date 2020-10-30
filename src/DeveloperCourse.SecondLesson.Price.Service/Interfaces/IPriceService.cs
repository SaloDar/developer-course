using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Price.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Price.Service.Interfaces
{
    public interface IPriceService
    {
        IEnumerable<PriceDto> GetAllPrices();

        IEnumerable<PriceDto> GetProductPrices(Guid productId);
    }
}