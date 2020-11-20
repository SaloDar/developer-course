using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Identity.API.DTOs;

namespace DeveloperCourse.SecondTask.Identity.API.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Creates the user with specified <paramref name="username"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="username">Username for user.</param>
        /// <param name="password">Password for the user.</param>
        /// <returns>User.</returns>
        Task<UserDto> Register(string username, string password);

        /// <summary>
        /// Returns access token when <param name="username"></param> and <param name="password"></param> is valid.
        /// </summary>
        /// <param name="username">User username.</param>
        /// <param name="password">User password.</param>
        /// <returns>Access token.</returns>
        Task<string> Authenticate(string username, string password);

        /// <summary>
        /// Returns the user from the current context.
        /// </summary>
        /// <returns>Current user.</returns>
        Task<UserDto> GetCurrentUser();
    }
}