using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.ThirdTask.Weather.API.Clients;
using DeveloperCourse.ThirdTask.Weather.API.Infrastructure.Configs;
using DeveloperCourse.ThirdTask.Weather.API.Infrastructure.MessageHandlers;
using DeveloperCourse.ThirdTask.Weather.API.Infrastructure.Middlewares;
using DeveloperCourse.ThirdTask.Weather.API.Interfaces;
using DeveloperCourse.ThirdTask.Weather.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace DeveloperCourse.ThirdTask.Weather.API
{
    public class Startup
    {
        #region Props

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        #endregion

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var weatherConfig = Configuration.GetSection("WeatherApi").Get<WeatherConfig>();

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();

            var refitSettings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
            };

            services.AddTransient(_ =>
                new AuthenticatedQueryMessageHandler(() =>
                    new KeyValuePair<string, string>("appid", weatherConfig.Token)));

            services.AddRefitClient<IOpenWeatherMapClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = weatherConfig.ApiLink;
                })
                .AddHttpMessageHandler<AuthenticatedQueryMessageHandler>();

            services.AddTransient<IWeatherService, WeatherService>();
            
            services.AddTransient<ApiErrorHandlingMiddleware>();

            #region Compression

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

                options.MimeTypes = new List<string>
                {
                    "text/plain", "text/json", "application/json"
                };
            });

            #endregion
            
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Weather API", Version = "v1"
                });
                
                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml"));

                swagger.EnableAnnotations();
                swagger.UseInlineDefinitionsForEnums();
                swagger.CustomSchemaIds(i => i.FullName);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            #endregion

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API");
                });
            }

            app.UseForwardedHeaders();
            
            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseMiddleware<ApiErrorHandlingMiddleware>();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}