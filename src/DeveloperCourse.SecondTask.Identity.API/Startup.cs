using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Web.Extensions;
using DeveloperCourse.SecondTask.Identity.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Identity.API.Infrastructure.Middlewares;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using DeveloperCourse.SecondTask.Identity.API.Services;
using DeveloperCourse.SecondTask.Identity.DataAccess.Context;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using DeveloperCourse.SecondTask.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

namespace DeveloperCourse.SecondTask.Identity.API
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

            var securityConfig = Configuration.GetSection("Security").Get<SecurityConfig>();

            services.Configure<WebApiConfig>(Configuration.GetSection("WebApi"));

            services.Configure<SecurityConfig>(Configuration.GetSection("Security"));

            #endregion

            services.AddAutoMapper(typeof(Startup));

            services.AddOptions();
            services.AddMemoryCache();

            services.AddDbContext<IdentityContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Identity")));

            services.AddIdentity<User, Role>()
                .AddUserStore<UserStore<User, Role, IdentityContext, Guid, IdentityUserClaim<Guid>, RoleUser,
                    IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>>()
                .AddRoleStore<RoleStore<Role, IdentityContext, Guid, RoleUser, IdentityRoleClaim<Guid>>>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

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
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IRoleManager, RoleManagerService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddHostedService<DbFillerService>();

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