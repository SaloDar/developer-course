using DeveloperCourse.SecondLesson.Infrastructure.Extensions;
using DeveloperCourse.SecondTask.Product.DataAccess.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondTask.Product.API.Infrastructure.Extensions
{
    public static class ProductDbOptionsExtensions
    {
        public static IServiceCollection AddProductDbOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.AddDbOptions<ProductDbOptions>(options => options.ConnectionString = config.GetConnectionString("Product"));
        }
    }
}