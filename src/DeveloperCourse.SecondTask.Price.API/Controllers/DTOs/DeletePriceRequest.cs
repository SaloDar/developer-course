using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Price.API.Controllers.DTOs
{
    public class DeletePriceRequest
    {
        /// <summary>
        /// Price identifier.
        /// </summary>
        /// <example>D300968F-428C-4BA7-86CD-F24AB7208599</example>
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}