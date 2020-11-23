using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Domain.Types;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeveloperCourse.SecondLesson.Common.Identity.DTOs
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
        public ICollection<UserRole> UserRoles { get; private set; }

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