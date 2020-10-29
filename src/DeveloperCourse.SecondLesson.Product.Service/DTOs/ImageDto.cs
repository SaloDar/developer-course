using System;

namespace DeveloperCourse.SecondLesson.Product.Service.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Uri Link { get; set; }
    }
}