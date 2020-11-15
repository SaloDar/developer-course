using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Clients.Image.DTOs.Responses
{
    public class GetImagesResponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}