using System;

namespace DeveloperCourse.SecondTask.Product.API.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Uri Link { get; set; }
    }
}