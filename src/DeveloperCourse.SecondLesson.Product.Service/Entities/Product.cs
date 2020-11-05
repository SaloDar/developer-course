using DeveloperCourse.SecondLesson.Shared.Entities;

namespace DeveloperCourse.SecondLesson.Product.Service.Entities
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
    }
}