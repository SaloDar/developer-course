using System;

namespace DeveloperCourse.SecondLesson.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime LastSavedDate { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            LastSavedDate = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void Deleted()
        {
            IsDeleted = true;
        }
    }
}