using System.Collections.Generic;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Clients.DTOs
{
    public class GetImagesDto
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}