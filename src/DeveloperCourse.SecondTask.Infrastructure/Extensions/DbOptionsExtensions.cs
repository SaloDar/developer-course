using System;
using DeveloperCourse.SecondTask.Infrastructure.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondTask.Infrastructure.Extensions
{
    public static class DbOptionsExtensions
    {
        public static IServiceCollection AddDbOptions<T>(this IServiceCollection serviceCollection, Action<T> setupAction) where T : DbOptions
        {
            return serviceCollection.Configure(setupAction);
        }
    }
}