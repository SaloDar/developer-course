using System;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Uri Link { get; set; }
    }
}