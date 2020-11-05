using DeveloperCourse.SecondLesson.Image.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondLesson.Image.Service.Infrastructure.Extensions
{
    public static class ImageDbOptionsExtensions
    {
        public static IServiceCollection AddImageDbOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.AddDbOptions<ImageDbOptions>(options => options.ConnectionString = config.GetConnectionString("Image"));
        }
    }
}