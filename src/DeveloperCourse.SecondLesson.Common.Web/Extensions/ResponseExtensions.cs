using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.SecondLesson.Common.Web.Extensions
{
    public static class ResponseExtensions
    {
        public static IServiceCollection AddCompression(this IServiceCollection services, params string[] mimeTypes)
        {
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;

                options.MimeTypes = new List<string>(mimeTypes)
                {
                    "text/plain", "text/json", "application/json"
                };
            });

            return services;
        }
    }
}