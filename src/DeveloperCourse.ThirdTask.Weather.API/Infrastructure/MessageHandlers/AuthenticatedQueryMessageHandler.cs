using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace DeveloperCourse.ThirdTask.Weather.API.Infrastructure.MessageHandlers
{
    public class AuthenticatedQueryMessageHandler : DelegatingHandler
    {
        private readonly Func<KeyValuePair<string, string>> _getAuthQuery;

        public AuthenticatedQueryMessageHandler(Func<KeyValuePair<string, string>> getAuthQuery)
        {
            _getAuthQuery = getAuthQuery;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _getAuthQuery();

            var newRequestUrl = QueryHelpers.AddQueryString(request.RequestUri!.ToString(), new[]
            {
                token
            });

            request.RequestUri = new Uri(newRequestUrl);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}