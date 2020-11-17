using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class UpdateProductRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromBody]
        public UpdateProductData Data { get; set; }
    }
}