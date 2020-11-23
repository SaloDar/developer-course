using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs.Requests;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity
{
    [Headers("Authorization: Bearer")]
    public interface IIdentityClient
    {
        [Get("/users/me")]
        Task<GetCurrentUserResponse> GetCurrentUser();

        [Post("/users/register")]
        Task<RegisterUserResponse> Register([Body] RegisterUserRequest request);

        [Post("/users/authenticate")]
        Task<AuthenticateUserResponse> Authenticate([Body] AuthenticateUserRequest request);
    }
}