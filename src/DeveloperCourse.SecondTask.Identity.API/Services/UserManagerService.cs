using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Identity.API.Services
{
    public class UserManagerService : UserManager<User>, IUserManagerService
    {
        #region Constructors

        public UserManagerService(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
        }

        #endregion

        #region Implemented Methods

        public new async Task<bool> CreateAsync(User user, string password)
        {
            var result = await base.CreateAsync(user, password);

            return result.Succeeded;
        }

        public new async Task<bool> AddToRoleAsync(User user, string role)
        {
            var result = await base.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public new async Task<bool> AddClaimAsync(User user, Claim claim)
        {
            var result = await base.AddClaimAsync(user, claim);

            return result.Succeeded;
        }

        public new async Task<bool> UpdateUserAsync(User user)
        {
            var result = await base.UpdateUserAsync(user);

            return result.Succeeded;
        }
        
        public async Task<User> FindByIdAsync(Guid userId)
        {
            return await base.FindByIdAsync(userId.ToString());
        }

        #endregion
    }
}