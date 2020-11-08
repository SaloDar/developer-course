using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class GetImageRequest
    {
        /// <summary>
        /// Image identifier.
        /// </summary>
        /// <example>B03644C2-B53C-443B-9FD7-F13C50B1E89C</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}