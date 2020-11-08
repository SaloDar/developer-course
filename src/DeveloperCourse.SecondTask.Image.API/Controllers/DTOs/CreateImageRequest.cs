using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class CreateImageRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [Required]
        [FromForm(Name = "productId")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [Required]
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}