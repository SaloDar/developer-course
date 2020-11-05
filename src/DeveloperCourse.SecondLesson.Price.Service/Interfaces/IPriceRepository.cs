using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Shared.Interfaces;

namespace DeveloperCourse.SecondLesson.Price.Service.Interfaces
{
    public interface IPriceRepository : IBaseRepository<Entities.Price>
    {
        Task<IEnumerable<Entities.Price>> GetPricesByProductId(Guid productId);
    }
}