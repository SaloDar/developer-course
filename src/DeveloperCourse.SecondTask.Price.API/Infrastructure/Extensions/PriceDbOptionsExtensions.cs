using DeveloperCourse.SecondTask.Price.DataAccess.Configs;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondTask.Price.API.Infrastructure.Extensions
{
    public static class PriceDbOptionsExtensions
    {
        public static IServiceCollection AddPriceDbOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.AddDbOptions<PriceDbOptions>(options => options.ConnectionString = config.GetConnectionString("Price"));
        }
    }
}