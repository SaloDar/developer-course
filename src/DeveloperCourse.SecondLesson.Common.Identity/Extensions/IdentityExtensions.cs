using System;
using DeveloperCourse.SecondLesson.Common.Identity.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeveloperCourse.SecondLesson.Common.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, SecurityConfig securityConfig)
        {
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

            return services;
        }
    }
}