using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs;

namespace DeveloperCourse.SecondTask.Market.Interfaces
{
    public interface IAuthService
    {
        Task<bool> IsAuth();

        Task<bool> Login(string username, string password);

        Task<bool> Register(string username, string password);

        Task Logout();

        Task<UserDto> GetCurrentUser();

        Task<string> GetToken();
    }
}