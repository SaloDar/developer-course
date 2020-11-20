using System;

namespace DeveloperCourse.SecondTask.Identity.API.DTOs
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
    }
}