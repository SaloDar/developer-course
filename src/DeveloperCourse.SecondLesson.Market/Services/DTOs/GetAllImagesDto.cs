using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class GetAllImagesDto
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}