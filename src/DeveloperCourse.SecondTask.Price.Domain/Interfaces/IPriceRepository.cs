using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Interfaces;
using Money;

namespace DeveloperCourse.SecondTask.Price.Domain.Interfaces
{
    public interface IPriceRepository : IBaseRepository<Domain.Entities.Price>
    {
        Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId);
        
        Task<bool> UpdateIsLastByProduct(Guid productId, Currency currency);
    }
}