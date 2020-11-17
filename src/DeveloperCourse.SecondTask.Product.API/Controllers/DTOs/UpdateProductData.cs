namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class UpdateProductData
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product SKU.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Product weight.
        /// </summary>
        public string Weight { get; set; }
    }
}