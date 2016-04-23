namespace DNSimple.Net.Common.Api
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class DNSimpleTokenHttpClientHandler : HttpClientHandler
    {
        private const string DNSimpleToken = "X-DNSimple-Token";

        private readonly string _username;
        private readonly string _token;

        private string Token => $"{_username}:{_token}";

        public DNSimpleTokenHttpClientHandler(string username, string token)
        {
            _username = username;
            _token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            request.Headers.Add(DNSimpleToken, Token);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}