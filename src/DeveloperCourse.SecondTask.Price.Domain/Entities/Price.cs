using System;
using DeveloperCourse.SecondLesson.Domain.Entities;
using Money;

namespace DeveloperCourse.SecondTask.Price.Domain.Entities
{
    public class Price : BaseEntity
    {
        public Guid ProductId { get; protected set; }
        
        public decimal Retail { get; protected set; }

        public decimal Cost { get; protected set; }

        public Currency Currency { get; protected set; }

        public bool IsLast { get; protected set; }

        protected Price()
        {
        }

        public Price(Guid productId, decimal retail, decimal cost, Currency currency)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Retail = retail;
            Cost = cost;
            Currency = currency;
            IsLast = true;
        }

        public void ChangeProduct(Guid productId)
        {
            ProductId = productId;
        }

        public void ChangeRetailPrice(decimal retailPrice)
        {
            Retail = retailPrice;
        }

        public void ChangeCostPrice(decimal costPrice)
        {
            Cost = costPrice;
        }

        public void ChangeCurrencyPrice(Currency currency)
        {
            Currency = currency;
        }
    }
}