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
        /// <param name="url">DNSimple compatible api url, defaults to https://api.dnsimple.com</param>
        public DNSimpleClient(string email, string token, Uri url = null)
            : this(new DNSimpleTokenHttpClientHandler(email, token), url)
        {
        }

        /// <summary>
        /// Authenticate with email-password combination using basic authorization.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">User's password</param>
        /// <param name="useBasic">True or false, there is no difference</param>
        /// <param name="url">DNSimple compatible api url, defaults to https://api.dnsimple.com</param>
        // ReSharper disable once UnusedParameter.Local
        public DNSimpleClient(string email, string password, bool useBasic, Uri url = null)
            : this(new BasicAuthenticationHttpClientHandler(email, password), url)
        {
        }

        /// <summary>
        /// Authenticate with a domain level token.
        /// </summary>
        /// <param name="token">Token for a single domain</param>
        /// <param name="url">DNSimple compatible api url, defaults to https://api.dnsimple.com</param>
        public DNSimpleClient(string token, Uri url = null)
            : this(new DNSimpleDomainTokenHttpClientHandler(token), url)
        {
        }

        private DNSimpleClient(HttpMessageHandler loginHandler, Uri uri)
        {
            _client = new HttpClient(loginHandler)
            {
                BaseAddress = uri ?? new Uri("https://api.dnsimple.com")
            };

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