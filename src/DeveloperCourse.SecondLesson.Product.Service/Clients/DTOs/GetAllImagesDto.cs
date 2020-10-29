using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Product.Service.Clients.DTOs
{
    public class GetAllImagesDto
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}