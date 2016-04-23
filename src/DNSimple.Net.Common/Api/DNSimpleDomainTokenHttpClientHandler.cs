namespace DNSimple.Net.Common.Api
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class DNSimpleDomainTokenHttpClientHandler : HttpClientHandler
    {
        private const string DNSimpleDomainToken = "X-DNSimple-Domain-Token";

        private readonly string _token;

        private string Token => $"{_token}";

        public DNSimpleDomainTokenHttpClientHandler(string token)
        {
            _token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            request.Headers.Add(DNSimpleDomainToken, Token);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}