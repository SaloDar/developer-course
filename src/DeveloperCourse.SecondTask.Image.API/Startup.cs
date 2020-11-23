using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Identity.Configs;
using DeveloperCourse.SecondLesson.Common.Identity.Extensions;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Common.Identity.Services;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Image.API.Clients;
using DeveloperCourse.SecondTask.Image.DataAccess.Context;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using DeveloperCourse.SecondTask.Image.API.Services;
using DeveloperCourse.SecondTask.Image.Domain.Interfaces;
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

namespace DeveloperCourse.SecondTask.Image.API
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

            var yandexDiskConfig = Configuration.GetSection("YandexDisk").Get<YandexDiskConfig>();
            
            var securityConfig = Configuration.GetSection("Security").Get<SecurityConfig>();

            services.Configure<WebApiConfig>(Configuration.GetSection("WebApi"));

            services.Configure<YandexDiskConfig>(Configuration.GetSection("YandexDisk"));

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();
            
            services.AddHttpContextAccessor();

            services.AddDbContext<ImageContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Image")));

            services.AddScoped<IImageContext, ImageContext>();
            
            services.AddJwtAuthentication(securityConfig);

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

            services.AddRefitClient<IYandexDiskClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = yandexDiskConfig.BaseUrl;
                    c.DefaultRequestHeaders.Authorization = yandexDiskConfig.AuthenticationHeader;
                });
            
            services.AddScoped<IUserContext, UserContext>();
            services.AddTransient<IDataStorageService, YandexDiskService>();
            services.AddTransient<IImageService, ImageService>();

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