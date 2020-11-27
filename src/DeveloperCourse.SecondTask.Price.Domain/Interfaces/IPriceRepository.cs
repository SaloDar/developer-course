using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Interfaces;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondTask.Price.Domain.Interfaces
{
    public interface IPriceRepository : IBaseRepository<Domain.Entities.Price>
    {
        Task<IEnumerable<Domain.Entities.Price>> GetAll(bool lasted);

        Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId);

        Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId, bool lasted);
        
        Task<bool> UpdateIsLastByProduct(Guid productId, Currency currency);
    }
}