using System;
using Money;

namespace DeveloperCourse.SecondTask.Market.Services.Price.DTOs.Requests
{
    public class CreatePriceRequest
    {
        public Guid ProductId { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal CostPrice { get; set; }

        public Currency Currency { get; set; }
    }
}