using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class CreateProductImageRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
    }
}