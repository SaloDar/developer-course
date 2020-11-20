using System;
using Microsoft.AspNetCore.Identity;

namespace DeveloperCourse.SecondTask.Identity.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        protected User()
        {
        }

        public User(string username)
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = username;
        }
    }
}