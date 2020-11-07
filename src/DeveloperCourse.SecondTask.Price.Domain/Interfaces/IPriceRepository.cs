using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Interfaces;

namespace DeveloperCourse.SecondTask.Price.Domain.Interfaces
{
    public interface IPriceRepository : IBaseRepository<Domain.Entities.Price>
    {
        Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId);
    }
}