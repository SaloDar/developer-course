using System;
using DeveloperCourse.SecondLesson.Domain.Entities;

namespace DeveloperCourse.SecondTask.Image.Domain.Entities
{
    public class Image : BaseEntity
    {
        #region Props

        /// <summary>
        /// Product identifier.
        /// </summary>
        public Guid ProductId { get; protected set; }

        /// <summary>
        /// Link to image.
        /// </summary>
        public string Link { get; protected set; }

        #endregion

        #region Constructors

        protected Image()
        {
        }

        public Image(Guid productId, Uri link)
        {
            ProductId = productId;
            Link = link.ToString();
        }

        #endregion

        #region Public Methods

        public void ChangeProduct(Guid productId)
        {
            ProductId = productId;
        }

        public void ChangeLink(Uri link)
        {
            Link = link.ToString();
        }

        #endregion
    }
}