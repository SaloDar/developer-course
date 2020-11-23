using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorStrap;
using BlazorStrap.Extensions;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Image;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Price;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Product;
using DeveloperCourse.SecondLesson.Common.Clients.MessageHandlers;
using DeveloperCourse.SecondTask.Market.Configs;
using DeveloperCourse.SecondTask.Market.Interfaces;
using DeveloperCourse.SecondTask.Market.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace DeveloperCourse.SecondTask.Market
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebAssemblyHost(args).Build().RunAsync();
        }

        private static WebAssemblyHostBuilder CreateWebAssemblyHost(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddBootstrapCss();
            builder.Services.AddSvgLoader();
            builder.Services.AddAntDesign();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient<IAuthService, AuthService>();

            var appConfiguration = builder.Configuration.Get<AppConfiguration>();

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

            builder.Services.AddTransient<AuthenticatedMessageHandler>(y =>
                new AuthenticatedMessageHandler(async () =>
                {
                    var authService = y.GetService<IAuthService>();

                    return await authService.GetToken();
                }));

            builder.Services.AddRefitClient<IProductClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Product;
                })
                .AddHttpMessageHandler<AuthenticatedMessageHandler>();

            builder.Services.AddRefitClient<IPriceClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Price;
                })
                .AddHttpMessageHandler<AuthenticatedMessageHandler>();

            builder.Services.AddRefitClient<IImageClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Image;
                })
                .AddHttpMessageHandler<AuthenticatedMessageHandler>();

            builder.Services.AddRefitClient<IIdentityClient>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Identity;
                })
                .AddHttpMessageHandler<AuthenticatedMessageHandler>();
            
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            return builder;
        }
    }
}