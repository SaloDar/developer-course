using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class DeleteImageRequest
    {
        /// <summary>
        /// Image identifier.
        /// </summary>
        /// <example>3BECFBAF-8104-410A-9DB3-C5C5489F22E3</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}