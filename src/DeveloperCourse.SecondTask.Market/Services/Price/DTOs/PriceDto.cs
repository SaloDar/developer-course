using System;
using Money;

namespace DeveloperCourse.SecondTask.Market.Services.Price.DTOs
{
    public class PriceDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public decimal Retail { get; set; }

        public Currency Currency { get; set; }
        
        public bool IsLast { get; set; }
    }
}