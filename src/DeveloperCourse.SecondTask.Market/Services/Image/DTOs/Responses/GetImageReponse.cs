using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.Image.DTOs.Responses
{
    public class GetImageReponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}