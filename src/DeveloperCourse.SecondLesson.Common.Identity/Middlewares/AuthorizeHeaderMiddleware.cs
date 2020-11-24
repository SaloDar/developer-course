using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondLesson.Common.Identity.Middlewares
{
    public class AuthorizeHeaderMiddleware : IMiddleware
    {
        #region Constants

        private const string HeaderKey = "X-Authorized";

        #endregion

        #region Implemented Methods

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var isAuthenticated = (context.User?.Identity?.IsAuthenticated ?? false).ToString();

            context.Response.Headers.Add(HeaderKey, isAuthenticated);

            await next(context);
        }

        #endregion
    }
}