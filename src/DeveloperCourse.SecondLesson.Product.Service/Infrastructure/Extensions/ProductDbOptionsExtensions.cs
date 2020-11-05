using DeveloperCourse.SecondLesson.Product.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondLesson.Product.Service.Infrastructure.Extensions
{
    public static class ProductDbOptionsExtensions
    {
        public static IServiceCollection AddProductDbOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.AddDbOptions<ProductDbOptions>(options => options.ConnectionString = config.GetConnectionString("Product"));
        }
    }
}