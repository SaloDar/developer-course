using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorStrap;
using BlazorStrap.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperCourse.ThirdLesson.View
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
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            return builder;
        }
    }
}