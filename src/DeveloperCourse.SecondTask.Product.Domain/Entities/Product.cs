using DeveloperCourse.SecondLesson.Domain.Entities;

namespace DeveloperCourse.SecondTask.Product.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Sku { get; protected set; }

        public string Weight { get; protected set; }

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
    }
}