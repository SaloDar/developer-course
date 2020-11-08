using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetImagesDto
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}