using AutoMapper;
using CorrelationId;
using CorrelationId.DependencyInjection;
using DeveloperCourse.SecondLesson.Common.Identity.Configs;
using DeveloperCourse.SecondLesson.Common.Identity.Extensions;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Common.Identity.Middlewares;
using DeveloperCourse.SecondLesson.Common.Identity.Services;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondLesson.Common.Web.Middlewares;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using DeveloperCourse.SecondTask.Price.API.Services;
using DeveloperCourse.SecondTask.Price.DataAccess.Repositories;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DeveloperCourse.SecondTask.Price.API
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
            #region Configs

            var webApiConfig = Configuration.GetSection("WebApi").Get<WebApiConfig>();

            services.Configure<WebApiConfig>(Configuration.GetSection("WebApi"));

            var securityConfig = Configuration.GetSection("Security").Get<SecurityConfig>();

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();
            
            services.AddHttpContextAccessor();

            services.AddPriceDbOptions(Configuration);

            services.AddJwtAuthentication(securityConfig);
            
            services.AddDefaultCorrelationId(options =>
            { 
                options.AddToLoggingScope = true;
                options.EnforceHeader = false;
                options.IgnoreRequestHeader = false;
                options.IncludeInResponse = true;
                options.UpdateTraceIdentifier = false;
            });

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddTransient<IPriceService, PriceService>();

            services.AddTransient<VersionHeaderMiddleware>();
            services.AddTransient<AuthorizeHeaderMiddleware>();
            services.AddTransient<ApiErrorHandlingMiddleware>();

            services.AddCors(x =>
                {
                    if (Environment.IsProduction())
                    {
                        x.AddDefaultPolicy(builder =>
                        {
                            builder.WithOrigins(webApiConfig.Domain)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .WithExposedHeaders("X-Authorized", "X-Correlation-ID", "X-Version");
                        });
                    }
                    else
                    {
                        x.AddDefaultPolicy(builder =>
                        {
                            builder.SetIsOriginAllowed(url => true)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .WithExposedHeaders("X-Authorized", "X-Correlation-ID", "X-Version");
                        });
                    }
                }
            );
            
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
            
            app.UseMiddleware<VersionHeaderMiddleware>();
            
            app.UseMiddleware<AuthorizeHeaderMiddleware>();
            
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