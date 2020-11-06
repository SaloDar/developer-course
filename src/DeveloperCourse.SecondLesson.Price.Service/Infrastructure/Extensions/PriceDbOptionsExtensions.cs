using DeveloperCourse.SecondLesson.Price.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondLesson.Price.Service.Infrastructure.Extensions
{
    public static class PriceDbOptionsExtensions
    {
        public static IServiceCollection AddPriceDbOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.AddDbOptions<PriceDbOptions>(options => options.ConnectionString = config.GetConnectionString("Price"));
        }
    }
}