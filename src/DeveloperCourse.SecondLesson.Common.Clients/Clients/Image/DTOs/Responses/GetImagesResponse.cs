using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Image.DTOs.Responses
{
    public class GetImagesResponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}