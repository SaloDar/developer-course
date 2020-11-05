using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Shared.Interfaces;

namespace DeveloperCourse.SecondLesson.Image.Service.Interfaces
{
    public interface IImageRepository : IBaseRepository<Entities.Image>
    {
        Task<IEnumerable<Entities.Image>> GetImageByProductId(Guid productId);
    }
}