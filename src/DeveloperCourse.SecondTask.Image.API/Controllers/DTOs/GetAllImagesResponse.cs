using System.Collections.Generic;
using DeveloperCourse.SecondTask.Image.API.DTOs;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class GetAllImagesResponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}