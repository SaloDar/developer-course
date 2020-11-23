using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Image.API.Clients;
using DeveloperCourse.SecondTask.Image.DataAccess.Context;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using DeveloperCourse.SecondTask.Image.API.Services;
using DeveloperCourse.SecondTask.Image.Domain.Interfaces;
using DeveloperCourse.SecondTask.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            services.AddMemoryCache();

            services.AddDbContext<ImageContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Image")));

            services.AddScoped<IImageContext, ImageContext>();
            
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

            services.AddHttpContextAccessor();

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = webApiConfig.ServiceName, Version = "v1"
                });
                
                var securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert only JWT",
                    Name = "JWT Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
                    }
                };

                swagger.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, Array.Empty<string>()
                    }
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