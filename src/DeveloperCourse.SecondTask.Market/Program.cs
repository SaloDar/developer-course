using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorStrap;
using BlazorStrap.Extensions;
using DeveloperCourse.SecondTask.Market.Configs;
using DeveloperCourse.SecondTask.Market.Services.Image;
using DeveloperCourse.SecondTask.Market.Services.Price;
using DeveloperCourse.SecondTask.Market.Services.Product;
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

            builder.Services.AddRefitClient<IProductService>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Product;
                });

            builder.Services.AddRefitClient<IPriceService>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Price;
                });

            builder.Services.AddRefitClient<IImageService>(refitSettings)
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = appConfiguration.ServicesRoutes.Image;
                });
            
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            return builder;
        }
    }
}