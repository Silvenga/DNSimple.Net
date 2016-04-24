namespace DNSimple.Net.V1.Api
{
    using System;
    using System.Net.Http;

    using DNSimple.Net.Common.Api;
    using DNSimple.Net.Common.Json;

    using Newtonsoft.Json;

    using Refit;

    public class DNSimpleClient
    {
        private readonly HttpClient _client;
        private readonly RefitSettings _settings;

        /// <summary>
        /// Authenticate with email-token combination.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="token">Api token</param>
        /// <param name="url">DNSimple compatible api url</param>
        public DNSimpleClient(string email, string token, string url = "https://api.dnsimple.com") : this()
        {
            var clientHandler = new DNSimpleTokenHttpClientHandler(email, token);
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

        /// <summary>
        /// Authenticate with email-password combination using basic authorization.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">User's password</param>
        /// <param name="useBasic">True or false, there is no difference</param>
        /// <param name="url">DNSimple compatible api url</param>
        // ReSharper disable once UnusedParameter.Local
        public DNSimpleClient(string email, string password, bool useBasic, string url = "https://api.dnsimple.com")
            : this()
        {
            var clientHandler = new BasicAuthenticationHttpClientHandler(email, password);
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url),
            };
        }

        /// <summary>
        /// Authenticate with a domain level token.
        /// </summary>
        /// <param name="token">Token for a single domain</param>
        /// <param name="url">DNSimple compatible api url</param>
        public DNSimpleClient(string token, string url = "https://api.dnsimple.com") : this()
        {
            var clientHandler = new DNSimpleDomainTokenHttpClientHandler(token);
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

        private DNSimpleClient()
        {
            _settings = new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver()
                }
            };
        }

        public IDomains Domains => RestService.For<IDomains>(_client, _settings);
        public IRecords Records => RestService.For<IRecords>(_client, _settings);
        public IZones Zones => RestService.For<IZones>(_client, _settings);
    }
}