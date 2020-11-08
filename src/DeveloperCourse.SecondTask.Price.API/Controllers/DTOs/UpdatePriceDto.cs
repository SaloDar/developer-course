using System;
using Money;

namespace DeveloperCourse.SecondTask.Price.API.Controllers.DTOs
{
    public class UpdatePriceDto
    {
        /// <summary>
        /// Product identifier, for filter the prices.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        public Guid? ProductId { get; set; } = null;

        /// <summary>
        /// Retail price.
        /// </summary>
        /// <example>100</example>
        public decimal? RetailPrice { get; set; } = null;

        /// <summary>
        /// Cost price
        /// </summary>
        /// <example>200</example>
        public decimal? CostPrice { get; set; } = null;

        /// <summary>
        /// Currency.
        /// </summary>
        /// <example>643</example>
        public Currency? Currency { get; set; } = null;
    }
}