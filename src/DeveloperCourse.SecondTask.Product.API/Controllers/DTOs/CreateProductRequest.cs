using System.ComponentModel.DataAnnotations;

namespace DeveloperCourse.SecondTask.Product.API.Controllers.DTOs
{
    public class CreateProductRequest
    {
        /// <summary>
        /// Product name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Product SKU.
        /// </summary>
        [Required]
        public string Sku { get; set; }

        /// <summary>
        /// Product weight.
        /// </summary>
        [Required]
        public string Weight { get; set; }
    }
}