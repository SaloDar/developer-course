using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Infrastructure.Identity;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using DeveloperCourse.SecondTask.Price.API.Services;
using DeveloperCourse.SecondTask.Price.DataAccess.Repositories;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;
using DeveloperCourse.SecondTask.Price.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DeveloperCourse.SecondTask.Price.API
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
            services.AddMemoryCache();

            services.AddPriceDbOptions(Configuration);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.RefreshOnIssuerKeyNotFound = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidIssuer = securityConfig.Issuer,
                        ValidAudiences = securityConfig.Audiences,
                        IssuerSigningKey = new SymmetricSecurityKey(securityConfig.SigningKeyBytes),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        TokenDecryptionKey = new SymmetricSecurityKey(securityConfig.EncryptionKeyBytes)
                    };
                });

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddTransient<IPriceService, PriceService>();

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

            services.AddHttpContextAccessor();

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