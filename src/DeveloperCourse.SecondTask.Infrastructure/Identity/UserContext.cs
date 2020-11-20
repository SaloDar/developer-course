using System;
using System.Linq;
using System.Security.Claims;
using DeveloperCourse.SecondLesson.Domain.Types;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Infrastructure.Identity
{
    public class UserContext : IUserContext
    {
        #region Props

        public IIdentityDto Identity { get; }

        public bool IsAuthenticated { get; }

        #endregion

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            IsAuthenticated = httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

            if (!IsAuthenticated)
            {
                Identity = default;

                return;
            }

            Identity = ParseUser(httpContextAccessor);
        }

        private IdentityDto ParseUser(IHttpContextAccessor httpContextAccessor)
        {
            var identity = new IdentityDto();

            var username = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

            var userRole = httpContextAccessor?.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => Enum.Parse<UserRole>(x.Value))
                .ToList();

            var userId = Guid.TryParse(httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
                ? id
                : default;

            identity.SetIdentity(userId, username, userRole);

            return identity;
        }
    }
}