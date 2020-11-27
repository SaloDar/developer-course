using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DeveloperCourse.SecondLesson.Common.Web.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string serviceName, string version = "v1")
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = serviceName, Version = version
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

                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml"));

                swagger.EnableAnnotations();
                swagger.UseInlineDefinitionsForEnums();
                swagger.CustomSchemaIds(i => i.FullName);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}