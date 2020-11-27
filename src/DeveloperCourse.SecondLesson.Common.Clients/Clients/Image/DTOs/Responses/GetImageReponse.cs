using System.Collections.Generic;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Image.DTOs.Responses
{
    public class GetImageReponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}