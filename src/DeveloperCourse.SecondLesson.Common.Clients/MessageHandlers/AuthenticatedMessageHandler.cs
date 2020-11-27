using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperCourse.SecondLesson.Common.Clients.MessageHandlers
{
    public class AuthenticatedMessageHandler : DelegatingHandler
    {
        private readonly Func<Task<string>> _getToken;

        public AuthenticatedMessageHandler(Func<Task<string>> getToken)
        {
            _getToken = getToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;

            if (auth != null)
            {
                var token = await _getToken();
                
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}