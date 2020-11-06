using System;
using DeveloperCourse.SecondLesson.Shared.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondLesson.Shared.Extensions
{
    public static class DbOptionsExtensions
    {
        public static IServiceCollection AddDbOptions<T>(this IServiceCollection serviceCollection, Action<T> setupAction) where T : DbOptions
        {
            return serviceCollection.Configure(setupAction);
        }
    }
}