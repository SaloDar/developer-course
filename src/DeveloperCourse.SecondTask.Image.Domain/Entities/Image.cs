using System;
using DeveloperCourse.SecondLesson.Domain.Entities;

namespace DeveloperCourse.SecondLesson.Image.Domain.Entities
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

        public void ChangeProduct(Guid productId)
        {
            ProductId = productId;
        }

        public void ChangeLink(Uri link)
        {
            Link = link.ToString();
        }
    }
}