using System;
using Money;

namespace DeveloperCourse.SecondTask.Product.API.DTOs
{
    public class PriceDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
    }
}