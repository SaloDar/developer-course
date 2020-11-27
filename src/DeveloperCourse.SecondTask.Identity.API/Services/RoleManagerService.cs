using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Identity.API.Services
{
    public class RoleManagerService : RoleManager<Role>, IRoleManager
    {
        #region Constructors

        public RoleManagerService(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        #endregion

        #region Implemented Methods

        public new async Task<bool> CreateAsync(Role role)
        {
            var result = await base.CreateAsync(role);

            return result.Succeeded;
        }

        public async Task<bool> RoleExistsAsync(UserRole role)
        {
            return await base.RoleExistsAsync(role.ToString());
        }

        #endregion
    }
}