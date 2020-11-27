using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public ICollection<UserRole> Roles { get; set; }
    }
}