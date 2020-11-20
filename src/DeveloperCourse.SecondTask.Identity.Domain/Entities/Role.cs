using System;
using DeveloperCourse.SecondLesson.Domain.Types;
using Microsoft.AspNetCore.Identity;

namespace DeveloperCourse.SecondTask.Identity.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        protected Role()
        {
        }

        public Role(UserRole role)
        {
            Id = Guid.NewGuid();
            Name = role.ToString();
        }
    }
}