using System.ComponentModel.DataAnnotations;

namespace DeveloperCourse.SecondTask.Identity.API.Controllers.DTOs
{
    public class AuthenticateUserRequest
    {
        /// <summary>
        /// Username.
        /// </summary>
        [Required]
        [RegularExpression("^[a-zA-Z0-9._@+-]{6,}$")]
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*()_+,.\\/;':\"-]).{8,}$")]
        public string Password { get; set; }
    }
}