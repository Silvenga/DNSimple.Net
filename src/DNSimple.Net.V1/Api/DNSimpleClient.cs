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

        public DNSimpleClient(string email, string password, string url = "https://api.dnsimple.com") : this()
        {
            var clientHandler = new DNSimpleTokenHttpClientHandler(email, password);
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

        // ReSharper disable once UnusedParameter.Local
        public DNSimpleClient(string email, string password, bool useBasic, string url = "https://api.dnsimple.com")
            : this()
        {
            var clientHandler = new BasicAuthenticationHttpClientHandler(email, password);
            _client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

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