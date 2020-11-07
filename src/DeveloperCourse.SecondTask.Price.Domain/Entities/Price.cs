using System;
using DeveloperCourse.SecondLesson.Domain.Entities;
using Money;

namespace DeveloperCourse.SecondTask.Price.Domain.Entities
{
    public class Price : BaseEntity
    {
        public Guid ProductId { get; protected set; }

        public decimal Amount { get; protected set; }

        public Currency Currency { get; protected set; }

        public bool IsLast { get; protected set; }

        protected Price()
        {
        }

        public Price(Guid productId, decimal amount, Currency currency)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Amount = amount;
            Currency = currency;
            IsLast = true;
        }
    }
}