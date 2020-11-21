using System;

namespace DeveloperCourse.SecondLesson.Domain.Entities
{
    public abstract class BaseEntity
    {
        #region Props

        /// <summary>
        /// Identifier.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Date of creation.
        /// </summary>
        public DateTime CreatedDate { get; protected set; }

        /// <summary>
        /// Date of last change.
        /// </summary>
        public DateTime LastSavedDate { get; protected set; }

        /// <summary>
        /// Created by user identifier.
        /// </summary>
        public Guid CreatedBy { get; protected set; }

        /// <summary>
        /// Is the entity removed.
        /// </summary>
        public bool IsDeleted { get; protected set; }

        #endregion

        #region Constructors

        protected BaseEntity()
        {
        }

        public BaseEntity(Guid createdBy)
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            LastSavedDate = DateTime.UtcNow;
            CreatedBy = createdBy;
            IsDeleted = false;
        }

        #endregion

        #region Public Methods

        public void Deleted()
        {
            IsDeleted = true;
        }

        public void Changed()
        {
            LastSavedDate = DateTime.UtcNow;
        }

        #endregion
    }
}