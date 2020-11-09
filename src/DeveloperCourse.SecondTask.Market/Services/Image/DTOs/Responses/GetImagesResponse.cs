using System.Collections.Generic;

namespace DeveloperCourse.SecondTask.Market.Services.Image.DTOs.Responses
{
    public class GetImagesResponse
    {
        public IEnumerable<ImageDto> Images { get; set; }
    }
}