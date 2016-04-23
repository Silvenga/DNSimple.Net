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

    public class DomainFacts : IDisposable
    {
        private static readonly Fixture AutoFixture = new Fixture();
        private readonly DNSimpleClient _dnSimpleClient;
        private string _mockDomain;

        private string MockDomainName
            => _mockDomain = _mockDomain ?? ("name" + AutoFixture.Create<string>() + ".cc").ToLower();

        public DomainFacts()
        {
            _dnSimpleClient = new DNSimpleClient(Constants.SandboxEmail, Constants.SandboxToken, Constants.SandboxUrl);
        }

        [Fact]
        public async Task Can_get_list_of_domains()
        {
            await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Domains.ListDomains();

            // Assert
            result.Should().Contain(x => x.Domain.Name == MockDomainName);
        }

        [Fact]
        public async Task When_new_domain_is_created_list_should_return_it()
        {
            var domain = new CreateDomainRequest
            {
                Domain = new CreateDomain
                {
                    Name = MockDomainName
                }
            };

            // Act
            var result = await _dnSimpleClient.Domains.CreateDomain(domain);

            // Assert
            result.Domain.Name.Should().Be(MockDomainName);

            var list = await _dnSimpleClient.Domains.ListDomains();
            list.Should().Contain(x => x.Domain.Name == MockDomainName);
        }

        [Fact]
        public async Task Can_delete()
        {
            await CreateDomain(MockDomainName);

            // Act
            await _dnSimpleClient.Domains.DeleteDomain(MockDomainName);

            // Assert
            var list = await _dnSimpleClient.Domains.ListDomains();
            list.Should().NotContain(x => x.Domain.Name == MockDomainName);
        }

        private async Task<ListDomainResult> CreateDomain(string domainName)
        {
            var domain = new CreateDomainRequest
            {
                Domain = new CreateDomain
                {
                    Name = domainName
                }
            };
            return await _dnSimpleClient.Domains.CreateDomain(domain);
        }

        public void Dispose()
        {
            if (_mockDomain != null)
            {
                try
                {
                    _dnSimpleClient.Domains.DeleteDomain(MockDomainName).Wait();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}