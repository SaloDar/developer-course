using System;

namespace DeveloperCourse.SecondLesson.Product.Service.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public string Weight { get; set; }

        public Product()
        {
        }
    }
}