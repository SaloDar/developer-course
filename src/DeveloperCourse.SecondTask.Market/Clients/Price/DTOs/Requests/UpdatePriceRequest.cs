using System;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondTask.Market.Clients.Price.DTOs.Requests
{
    public class UpdatePriceRequest
    {
        public Guid? ProductId { get; set; } = null;

        public decimal? RetailPrice { get; set; } = null;

        public decimal? CostPrice { get; set; } = null;

        public Currency? Currency { get; set; } = null;
    }
}