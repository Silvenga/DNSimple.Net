namespace DNSimple.V1.Api
{
    using System;
    using System.Net.Http;

    using DNSimple.Common.Api;
    using DNSimple.Common.Json;

    using Newtonsoft.Json;

    using Refit;

    public class Client
    {
        private readonly HttpClient _client;
        private readonly RefitSettings _settings;

        public Client(string email, string token, string url = "https://api.dnsimple.com")
        {
            var tokenHttpClientHandler = new DNSimpleTokenHttpClientHandler(email, token);
            _client = new HttpClient(tokenHttpClientHandler)
            {
                BaseAddress = new Uri(url)
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
    }
}