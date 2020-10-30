using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Image.Service.Interfaces
{
    public interface IMemoryStore
    {
        IEnumerable<Entities.Image> Images { get; }
    }
}