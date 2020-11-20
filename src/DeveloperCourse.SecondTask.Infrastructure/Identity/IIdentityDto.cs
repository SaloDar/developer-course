using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondTask.Infrastructure.Identity
{
    public interface IIdentityDto
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Usermame.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// User roles.
        /// </summary>
        List<UserRole> UserRoles { get; }

        public void SetIdentity(Guid userId, string username, List<UserRole> userRoles);

        public void SetIdentity(IIdentityDto identity);
    }
}