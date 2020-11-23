using System;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Price.DTOs.Requests
{
    public class CreatePriceRequest
    {
        public Guid ProductId { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal CostPrice { get; set; }

        public Currency Currency { get; set; }
    }
}