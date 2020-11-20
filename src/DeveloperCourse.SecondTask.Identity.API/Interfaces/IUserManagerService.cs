using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;

namespace DeveloperCourse.SecondTask.Identity.API.Interfaces
{
    public interface IUserManagerService
    {
        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <returns>User matching the specified <paramref name="userName"/> if it exists.</returns>
        Task<User> FindByNameAsync(string userName);

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The user id to search for.</param>
        /// <returns>User matching the specified <paramref name="userId"/> if it exists.</returns>
        Task<User> FindByIdAsync(Guid userId);

        /// <summary>
        /// Creates the specified <paramref name="user"/> in the backing store with given <paramref name="password"/>.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user to hash and store.</param>
        /// <returns>Whether the operation was successful.</returns>
        Task<bool> CreateAsync(User user, string password);

        /// <summary>
        /// Returns a flag indicating whether the given <paramref name="password"/> is valid for the
        /// specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose password should be validated.</param>
        /// <param name="password">The password to validate</param>
        /// <returns>True if the specified <paramref name="password" /> matches the one store for
        /// the <paramref name="user"/>, otherwise false.</returns>
        Task<bool> CheckPasswordAsync(User user, string password);

        /// <summary>
        /// Gets a list of role names the specified <paramref name="user"/> belongs to.
        /// </summary>
        /// <param name="user">The user whose role names to retrieve.</param>
        /// <returns>Collection of roles.</returns>
        Task<IList<string>> GetRolesAsync(User user);

        /// <summary>
        /// Add the specified <paramref name="user"/> to the role.
        /// </summary>
        /// <param name="user">The user to add to the role.</param>
        /// <param name="role">The role to add the user to.</param>
        /// <returns>Whether the operation was successful.</returns>
        Task<bool> AddToRoleAsync(User user, string role);

        /// <summary>
        /// Adds the specified <paramref name="claim"/> to the <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claim">The claim to add.</param>
        /// <returns>Whether the operation was successful.</returns>
        Task<bool> AddClaimAsync(User user, Claim claim);

        /// <summary>
        /// Called to update the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Whether the operation was successful.</returns>
        Task<bool> UpdateUserAsync(User user);
    }
}