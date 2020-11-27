using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DeveloperCourse.SecondTask.Identity.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        #region Fileds

        private readonly List<RoleUser> _roles = new List<RoleUser>();

        #endregion

        #region Props

        /// <summary>
        /// User roles.
        /// </summary>
        public virtual ICollection<RoleUser> Roles => _roles.AsReadOnly();

        #endregion

        #region Constructors

        protected User()
        {
        }

        public User(string username)
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = username;
        }

        #endregion
    }
}