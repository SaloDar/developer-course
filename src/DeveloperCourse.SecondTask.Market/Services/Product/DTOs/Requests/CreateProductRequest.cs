namespace DeveloperCourse.SecondTask.Market.Services.Product.DTOs.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public string Weight { get; set; }
    }
}