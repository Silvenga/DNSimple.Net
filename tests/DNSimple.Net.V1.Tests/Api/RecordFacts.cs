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
            await CreateDomainAsync(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Records.ListRecordsByDomainNameAsync(MockDomainName);

            // Assert
            result.Where(x => x.RecordType == "NS").Should().HaveCount(4);
        }

        [Fact]
        public async Task Can_get_list_of_records_by_domain_id()
        {
            var domain = await CreateDomainAsync(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Records.ListRecordsByDomainIdAsync(domain.Id);

            // Assert
            result.Where(x => x.RecordType == "NS").Should().HaveCount(4);
        }

        [Fact]
        public async Task Can_create_record_by_domain_name()
        {
            await CreateDomainAsync(MockDomainName);
            var request = new RecordRequest
            {
                RecordType = "TXT",
                Name = "",
                Content = AutoFixture.Create<string>()
            };

            // Act
            await _dnSimpleClient.Records.CreateRecordByDomainNameAsync(MockDomainName, request);

            // Assert
            var result = await _dnSimpleClient.Records.ListRecordsByDomainNameAsync(MockDomainName);
            result.Should().Contain(x => x.Content == request.Content);
        }

        [Fact]
        public async Task Can_get_record_by_domain_name()
        {
            await CreateDomainAsync(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecordAsync(content);

            // Act
            var result = await _dnSimpleClient.Records.GetRecordByDomainNameAsync(MockDomainName, record.Id);

            // Assert
            result.Should().NotBeNull();
            result.Content.Should().Be(content);
        }

        [Fact]
        public async Task Can_update_record_by_domain_name()
        {
            await CreateDomainAsync(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecordAsync(content);
            var updateRequest = new RecordRequest
            {
                Name = "",
                Content = AutoFixture.Create<string>()
            };

            // Act
            await _dnSimpleClient.Records.UpdateRecordByDomainNameAsync(MockDomainName, record.Id, updateRequest);

            // Assert
            var result = await _dnSimpleClient.Records.GetRecordByDomainNameAsync(MockDomainName, record.Id);
            result.Content.Should().Be(updateRequest.Content);
        }

        [Fact]
        public async Task Can_delete_by_domain_name()
        {
            await CreateDomainAsync(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecordAsync(content);

            // Act
            await _dnSimpleClient.Records.DeleteRecordByDomainNameAsync(MockDomainName, record.Id);

            // Assert
            var result = await _dnSimpleClient.Records.ListRecordsByDomainNameAsync(MockDomainName);
            result.Should().NotContain(x => x.Id == record.Id);
        }

        private async Task<DomainResult> CreateDomainAsync(string domainName)
        {
            var domain = new DomainRequest(domainName);
            return await _dnSimpleClient.Domains.CreateDomainAsync(domain);
        }

        private async Task<RecordResponse> CreateRecordAsync(string context)
        {
            var request = new RecordRequest
            {
                RecordType = "TXT",
                Name = "",
                Content = context
            };
            return await _dnSimpleClient.Records.CreateRecordByDomainNameAsync(MockDomainName, request);
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