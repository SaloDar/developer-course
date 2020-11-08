using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class UpdateImageRequest
    {
        /// <summary>
        /// Image identifier.
        /// </summary>
        /// <example>3BECFBAF-8104-410A-9DB3-C5C5489F22E3</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [FromForm]
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        [FromForm]
        public IFormFile File { get; set; }
    }
}