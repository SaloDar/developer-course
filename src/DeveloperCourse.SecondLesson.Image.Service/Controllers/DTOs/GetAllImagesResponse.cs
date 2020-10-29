using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Image.Service.Controllers.DTOs
{
    public class GetAllImagesResponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}