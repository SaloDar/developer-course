using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Infrastructure.Identity;
using DeveloperCourse.SecondTask.Product.API.Clients;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Product.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
using DeveloperCourse.SecondTask.Product.API.Services;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using DeveloperCourse.SecondTask.Product.DataAccess.Context;
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
            services.AddMemoryCache();
            
            services.AddDbContext<ProductContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Product")));
            
            services.AddScoped<IProductContext, ProductContext>();
            
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

            services.AddScoped<IUserContext, UserContext>();
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