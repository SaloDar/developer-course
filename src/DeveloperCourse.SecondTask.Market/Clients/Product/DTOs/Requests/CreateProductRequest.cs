namespace DeveloperCourse.SecondTask.Market.Clients.Product.DTOs.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public string Weight { get; set; }
    }
}