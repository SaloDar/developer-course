using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondLesson.Common.Clients.MessageHandlers
{
    public class ForwardAuthenticateTokenMessageHandler : DelegatingHandler
    {
        #region Consts

        private const string AuthorizationKey = "Authorization";

        #endregion
        
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructors

        public ForwardAuthenticateTokenMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Ovveriden Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            
            if (auth != null)
            {
                var tokenWithScheme = _httpContextAccessor.HttpContext.Request.Headers
                    .FirstOrDefault(x => x.Key.Equals(AuthorizationKey, StringComparison.OrdinalIgnoreCase))
                    .Value
                    .FirstOrDefault(y => y.StartsWith(auth.Scheme, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(tokenWithScheme))
                {
                    request.Headers.Remove(AuthorizationKey);
                    request.Headers.Add(AuthorizationKey, tokenWithScheme);
                }
            }
            
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}