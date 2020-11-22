using System;
using System.ComponentModel.DataAnnotations;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondTask.Price.API.Controllers.DTOs
{
    public class CreatePriceRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Retail price.
        /// </summary>
        /// <example>100</example>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// Cost price
        /// </summary>
        /// <example>200</example>
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        /// <example>643</example>
        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
    }
}