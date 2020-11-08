using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetImageDto
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}