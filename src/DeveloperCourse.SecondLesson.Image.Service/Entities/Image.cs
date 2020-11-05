using System;
using DeveloperCourse.SecondLesson.Shared.Entities;

namespace DeveloperCourse.SecondLesson.Image.Service.Entities
{
    public class Image : BaseEntity
    {
        public Guid ProductId { get; protected set; }

        public string Link { get; protected set; }

        protected Image()
        {
        }

        public Image(Guid productId, Uri link)
        {
            ProductId = productId;
            Link = link.ToString();
        }
    }
}