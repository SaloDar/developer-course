using System;

namespace DeveloperCourse.SecondTask.Market.Services.Image.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Uri Link { get; set; }
    }
}