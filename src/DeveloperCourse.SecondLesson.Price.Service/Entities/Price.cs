using System;
using Money;

namespace DeveloperCourse.SecondLesson.Price.Service.Entities
{
    public class Price
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public Price()
        {
        }
    }
}