using System;
using DeveloperCourse.SecondLesson.Domain.Types;
using Microsoft.AspNetCore.Identity;

namespace DeveloperCourse.SecondTask.Identity.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        #region Props

        /// <summary>
        /// Role value.
        /// </summary>
        public UserRole Value { get; protected set; }

        #endregion

        #region Constructors

        protected Role()
        {
        }

        public Role(UserRole role)
        {
            Id = Guid.NewGuid();
            Name = role.ToString();
            Value = role;
        }

        #endregion
    }
}