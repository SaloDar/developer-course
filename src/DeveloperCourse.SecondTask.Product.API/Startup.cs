using AutoMapper;
using CorrelationId;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Image;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Price;
using DeveloperCourse.SecondLesson.Common.Clients.MessageHandlers;
using DeveloperCourse.SecondLesson.Common.Identity.Configs;
using DeveloperCourse.SecondLesson.Common.Identity.Extensions;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Common.Identity.Services;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
using DeveloperCourse.SecondTask.Product.API.Services;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using DeveloperCourse.SecondTask.Product.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
            
            var securityConfig = Configuration.GetSection("Security").Get<SecurityConfig>();

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();
            
            services.AddHttpContextAccessor();
            
            services.AddTransient<ForwardAuthenticateTokenMessageHandler>();
            
            services.AddScoped<IUserContext, UserContext>();

            services.AddDbContext<ProductContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Product")));
            
            services.AddScoped<IProductContext, ProductContext>();

            services.AddJwtAuthentication(securityConfig);

            services.AddDefaultCorrelationId(options =>
            { 
                options.AddToLoggingScope = true;
                options.EnforceHeader = false;
                options.IgnoreRequestHeader = false;
                options.IncludeInResponse = true;
                options.UpdateTraceIdentifier = false;
            });
            
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
                })
                .AddCorrelationIdForwarding()
                .AddHttpMessageHandler<ForwardAuthenticateTokenMessageHandler>();

            services.AddRefitClient<IImageClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = webApiConfig.Routes.ImageApi;
                })
                .AddCorrelationIdForwarding()
                .AddHttpMessageHandler<ForwardAuthenticateTokenMessageHandler>();

            services.AddTransient<IProductService, ProductService>();
            
            services.AddTransient<ApiErrorHandlingMiddleware>();

            services.AddCors(options =>
                options.AddDefaultPolicy(x =>
                    x.SetIsOriginAllowed(url => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));
            
            services.AddCompression();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            
            services.AddSwagger(webApiConfig.ServiceName);
            
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
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
            
            app.UseForwardedHeaders();

            app.UseCors();
            
            app.UseCorrelationId();

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