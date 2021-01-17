using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RhipeApi.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler
        : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var api_key = "api-key";
            var api_keyValue = "API-1UOLMM7RVIWZ0ML";
            
            //I would have added this in the authentication header but the current api is simply looking for key in the header of the request
            if (!request.Headers.Contains(api_key))
            {
                request.Headers.Add(api_key, api_keyValue);
            }
            //if (token != null)
            //{
            //    request.Headers.Authorization = new AuthenticationHeaderValue("api-key", token);
            //}

            return await base.SendAsync(request, cancellationToken);
        }

        
    }
}
