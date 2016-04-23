namespace DNSimple.Net.Common.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class BasicAuthenticationHttpClientHandler : HttpClientHandler
    {
        private readonly string _username;
        private readonly string _password;

        public BasicAuthenticationHttpClientHandler(string username, string password)
        {
            _username = username;
            _password = password;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            Credentials = new NetworkCredential
            {
                UserName = _username,
                Password = _password
            };
            
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}