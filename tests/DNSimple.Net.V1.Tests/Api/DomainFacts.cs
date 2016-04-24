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
            var result = await _dnSimpleClient.Domains.ListDomainsAsync();

            // Assert
            result.Should().Contain(x => x.Name == MockDomainName);
        }

        [Fact]
        public async Task Can_get_a_single_domain()
        {
            var domain = await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Domains.GetDomainAsync(MockDomainName);

            // Assert
            result.Name.Should().Be(MockDomainName);
            result.Id.Should().Be(domain.Id);
            result.Lockable.Should().Be(domain.Lockable);
        }

        [Fact]
        public async Task When_new_domain_is_created_list_should_return_it()
        {
            var domain = new DomainRequest(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Domains.CreateDomainAsync(domain);

            // Assert
            result.Name.Should().Be(MockDomainName);

            var list = await _dnSimpleClient.Domains.ListDomainsAsync();
            list.Should().Contain(x => x.Name == MockDomainName);
        }

        [Fact]
        public async Task Can_delete()
        {
            await CreateDomain(MockDomainName);

            // Act
            await _dnSimpleClient.Domains.DeleteDomainAsync(MockDomainName);

            // Assert
            var list = await _dnSimpleClient.Domains.ListDomainsAsync();
            list.Should().NotContain(x => x.Name == MockDomainName);
        }

        [Fact(Skip = "Only for cleanup")]
        public async Task Delete_all()
        {
            var list = await _dnSimpleClient.Domains.ListDomainsAsync();

            // Act
            foreach (var listDomainResult in list)
            {
                await _dnSimpleClient.Domains.DeleteDomainAsync(listDomainResult.Name);
            }
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