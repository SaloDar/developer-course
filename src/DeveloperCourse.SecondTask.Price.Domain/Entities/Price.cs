using System;
using DeveloperCourse.SecondLesson.Domain.Entities;
using Money;

namespace DeveloperCourse.SecondTask.Price.Domain.Entities
{
    public class Price : BaseEntity
    {
        #region Props

        /// <summary>
        /// Product identifier.
        /// </summary>
        public Guid ProductId { get; protected set; }

        /// <summary>
        /// Retail price.
        /// </summary>
        public decimal Retail { get; protected set; }

        /// <summary>
        /// Cost price.
        /// </summary>
        public decimal Cost { get; protected set; }

        /// <summary>
        /// Currency.
        /// </summary>
        public Currency Currency { get; protected set; }

        /// <summary>
        /// Is the last active price.
        /// </summary>
        public bool IsLast { get; protected set; }

        #endregion

        #region Constructors

        protected Price()
        {
        }

        public Price(Guid productId, decimal retail, decimal cost, Currency currency, Guid userId) : base(userId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Retail = retail;
            Cost = cost;
            Currency = currency;
            IsLast = true;
        }

        #endregion

        #region Public Methods

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

        #endregion
    }
}