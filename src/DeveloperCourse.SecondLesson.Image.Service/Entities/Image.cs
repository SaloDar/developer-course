using System;

namespace DeveloperCourse.SecondLesson.Image.Service.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Uri Link { get; set; }

        public Image()
        {
        }
    }
}