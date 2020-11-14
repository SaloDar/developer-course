using DeveloperCourse.SecondLesson.Domain.Entities;

namespace DeveloperCourse.SecondTask.Product.Domain.Entities
{
    public class Product : BaseEntity
    {
        #region Props

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Product SKU.
        /// </summary>
        public string Sku { get; protected set; }

        /// <summary>
        /// Product weight.
        /// </summary>
        public string Weight { get; protected set; }

        #endregion

        #region Constructors

        protected Product()
        {
        }

        public Product(string name, string description, string sku, string weight)
        {
            Name = name;
            Description = description;
            Sku = sku;
            Weight = weight;
        }

        #endregion

        #region Public Methods

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void ChangeSKU(string sku)
        {
            Sku = sku;
        }

        public void ChangeWeight(string weight)
        {
            Weight = weight;
        }

        #endregion
    }
}