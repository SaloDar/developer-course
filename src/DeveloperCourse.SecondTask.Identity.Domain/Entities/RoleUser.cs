using System;
using Microsoft.AspNetCore.Identity;

namespace DeveloperCourse.SecondTask.Identity.Domain.Entities
{
    public class RoleUser : IdentityUserRole<Guid>
    {
        #region Props

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

        #endregion

        #region Constructors

        public RoleUser()
        {
        }

        #endregion
    }
}