using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondLesson.Common.Web.Middlewares
{
    public class VersionHeaderMiddleware : IMiddleware
    {
        #region Constants

        private const string HeaderKey = "X-Version";

        #endregion

        #region Implemented Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? string.Empty;

            context.Response.Headers.Add(HeaderKey, version);

            await next(context);
        }

        #endregion
    }
}