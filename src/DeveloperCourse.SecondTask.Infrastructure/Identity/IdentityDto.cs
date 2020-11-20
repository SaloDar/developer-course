using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DeveloperCourse.SecondLesson.Domain.Types;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DeveloperCourse.SecondTask.Infrastructure.Identity
{
    public class IdentityDto : IIdentityDto
    {
        [JsonIgnore]
        [IgnoreDataMember]
        [BindNever]
        public Guid UserId { get; private set; }

        [JsonIgnore]
        [BindNever]
        public string Username { get; private set; }

        [JsonIgnore]
        [BindNever]
        public List<UserRole> UserRoles { get; private set; }

        public void SetIdentity(Guid userId, string username, List<UserRole> userRoles)
        {
            UserId = userId;
            Username = username;
            UserRoles = userRoles;
        }

        public void SetIdentity(IIdentityDto identity)
        {
            UserId = identity.UserId;
            Username = identity.Username;
            UserRoles = identity.UserRoles;
        }
    }
}