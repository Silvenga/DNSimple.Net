namespace DNSimple.Net.V1.Tests.Api
{
    using System;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Api;
    using DNSimple.Net.V1.Models;
    using DNSimple.Net.V1.Tests.Helpers;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class ZoneFacts : IDisposable
    {
        private static readonly Fixture AutoFixture = new Fixture();
        private readonly DNSimpleClient _dnSimpleClient;
        private string _mockDomain;

        private string MockDomainName
            => _mockDomain = _mockDomain ?? ("name" + AutoFixture.Create<string>() + ".cc").ToLower();

        public ZoneFacts()
        {
            _dnSimpleClient = new DNSimpleClient(Constants.SandboxEmail, Constants.SandboxToken, Constants.SandboxUrl);
        }

        [Fact]
        public async Task Can_get_list_of_records_by_domain_name()
        {
            await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Zones.ExportByDomainNameAsync(MockDomainName);

            // Assert
            result.Zone.Should().Contain($"$ORIGIN {MockDomainName}.");
        }

        private async Task<DomainResult> CreateDomain(string domainName)
        {
            var domain = new DomainRequest(domainName);
            return await _dnSimpleClient.Domains.CreateDomainAsync(domain);
        }

        public void Dispose()
        {
            if (_mockDomain != null)
            {
                try
                {
                    _dnSimpleClient.Domains.DeleteDomainAsync(MockDomainName).Wait();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}