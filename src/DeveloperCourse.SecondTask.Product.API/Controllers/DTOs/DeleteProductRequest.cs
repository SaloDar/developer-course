using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class DeleteProductRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>6766dc57-da6e-41ab-9f7e-746b5a99eb14</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}