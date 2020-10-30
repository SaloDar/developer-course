using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Price.Service.Interfaces
{
    public interface IMemoryStore
    {
        IEnumerable<Entities.Price> Prices { get; }
    }
}