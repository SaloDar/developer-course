using System;
using System.ComponentModel.DataAnnotations;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondTask.Price.API.Controllers.DTOs
{
    public class UpdatePriceDto
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        public Guid? ProductId { get; set; } = null;

        /// <summary>
        /// Retail price.
        /// </summary>
        /// <example>100</example>
        [Range(0, double.MaxValue)]
        public decimal? RetailPrice { get; set; } = null;

        /// <summary>
        /// Cost price
        /// </summary>
        /// <example>200</example>
        [Range(0, double.MaxValue)]
        public decimal? CostPrice { get; set; } = null;

        /// <summary>
        /// Currency.
        /// </summary>
        /// <example>643</example>
        [EnumDataType(typeof(Currency))]
        public Currency? Currency { get; set; } = null;
    }
}