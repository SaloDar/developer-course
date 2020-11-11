using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class GetProductRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>6766DC57-DA6E-41AB-9F7E-746B5A99EB14</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}