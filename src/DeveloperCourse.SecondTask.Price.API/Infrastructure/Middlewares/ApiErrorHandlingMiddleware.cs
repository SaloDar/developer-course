using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DeveloperCourse.SecondTask.Price.API.Infrastructure.Middlewares
{
    public class ApiErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ApiErrorHandlingMiddleware> _logger;

        private readonly IHostEnvironment _environment;

        public ApiErrorHandlingMiddleware(ILogger<ApiErrorHandlingMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _environment = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError("Unhandled API exception.", ex);

            var statusCode = HttpStatusCode.InternalServerError;

            string exceptionMessage = null;
            var message = "Что-то пошло не так...\nПопробуйте еще раз.";

            if (!_environment.IsProduction())
            {
                exceptionMessage = ex.Message;
            }

            context.Response.StatusCode = (int) statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode, Exception = exceptionMessage, Message = message
            }, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(true, false)
                }
            }));
        }
    }
}