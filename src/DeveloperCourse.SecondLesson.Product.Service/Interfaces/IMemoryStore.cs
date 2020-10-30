using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Product.Service.Interfaces
{
    public interface IMemoryStore
    {
        IEnumerable<Entities.Product> Products { get; }
    }
}