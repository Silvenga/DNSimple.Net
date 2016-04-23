namespace DNSimple.Net.V1.Tests.Api
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Api;
    using DNSimple.Net.V1.Models;
    using DNSimple.Net.V1.Tests.Helpers;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class RecordFacts : IDisposable
    {
        private static readonly Fixture AutoFixture = new Fixture();
        private readonly DNSimpleClient _dnSimpleClient;
        private string _mockDomain;

        private string MockDomainName
            => _mockDomain = _mockDomain ?? ("name" + AutoFixture.Create<string>() + ".cc").ToLower();

        public RecordFacts()
        {
            _dnSimpleClient = new DNSimpleClient(Constants.SandboxEmail, Constants.SandboxToken, Constants.SandboxUrl);
        }

        [Fact]
        public async Task Can_get_list_of_records_by_domain_name()
        {
            await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Records.ListRecordsByName(MockDomainName);

            // Assert
            result.Where(x => x.Record.RecordType == "NS").Should().HaveCount(4);
        }

        [Fact]
        public async Task Can_get_list_of_records_by_domain_id()
        {
            var domain = await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Records.ListRecordsById(domain.Domain.Id);

            // Assert
            result.Where(x => x.Record.RecordType == "NS").Should().HaveCount(4);
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