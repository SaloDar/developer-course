using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.SecondTask.Product.API.Clients;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
using DeveloperCourse.SecondTask.Product.API.Services;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Extensions;
using DeveloperCourse.SecondTask.Product.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace DeveloperCourse.SecondTask.Product.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Configs

            var webApiConfig = Configuration.GetSection("WebApi").Get<WebApiConfig>();

            services.Configure<WebApiConfig>(Configuration.GetSection("WebApi"));

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();
            services.AddMemoryCache();
            
            services.AddProductDbOptions(Configuration);

            services.AddScoped<IProductRepository, ProductRepository>();

            var refitSettings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        ContractResolver = new DefaultContractResolver()
                    })
            };

            services.AddRefitClient<IPriceClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = webApiConfig.Routes.PriceApi;
                });

            services.AddRefitClient<IImageClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = webApiConfig.Routes.ImageApi;
                });

            services.AddTransient<ApiErrorHandlingMiddleware>();

            services.AddTransient<IProductService, ProductService>();
            
            services.AddCors(options =>
                options.AddDefaultPolicy(x =>
                    x.SetIsOriginAllowed(url => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));

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

                options.MimeTypes = new[]
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
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddHttpContextAccessor();

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = webApiConfig.ServiceName, Version = "v1"
                });

                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                swagger.EnableAnnotations();
                swagger.UseInlineDefinitionsForEnums();
                swagger.CustomSchemaIds(i => i.FullName);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<WebApiConfig> webApiConfig)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", webApiConfig.Value.ServiceName);
                });
            }

            app.UseCors();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();

            app.UseMiddleware<ApiErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();
        }
    }
}