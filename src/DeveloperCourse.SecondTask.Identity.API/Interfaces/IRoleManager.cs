using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;

namespace DeveloperCourse.SecondTask.Identity.API.Interfaces
{
    public interface IRoleManager
    {
        /// <summary>
        /// Creates the specified <paramref name="role"/> in the persistence store.
        /// </summary>
        /// <param name="role">The role to create.</param>
        /// <returns>Whether the operation was successful.</returns>
        Task<bool> CreateAsync(Role role);

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="role"/> exists.
        /// </summary>
        /// <param name="role">The role name whose existence should be checked.</param>
        /// <returns>True if the role name exists, otherwise false.</returns>
        Task<bool> RoleExistsAsync(UserRole role);
    }
}