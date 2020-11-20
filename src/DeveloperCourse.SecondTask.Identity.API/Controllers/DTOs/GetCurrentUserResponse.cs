using DeveloperCourse.SecondTask.Identity.API.DTOs;

namespace DeveloperCourse.SecondTask.Identity.API.Controllers.DTOs
{
    public class GetCurrentUserResponse
    {
        /// <summary>
        /// User.
        /// </summary>
        public UserDto User { get; set; }
    }
}