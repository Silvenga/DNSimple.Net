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
            var result = await _dnSimpleClient.Records.ListRecordsByDomainName(MockDomainName);

            // Assert
            result.Where(x => x.Record.RecordType == "NS").Should().HaveCount(4);
        }

        [Fact]
        public async Task Can_get_list_of_records_by_domain_id()
        {
            var domain = await CreateDomain(MockDomainName);

            // Act
            var result = await _dnSimpleClient.Records.ListRecordsByDomainId(domain.Domain.Id);

            // Assert
            result.Where(x => x.Record.RecordType == "NS").Should().HaveCount(4);
        }

        [Fact]
        public async Task Can_create_record_by_domain_name()
        {
            await CreateDomain(MockDomainName);
            var request = new CreateRecordRequest
            {
                Record = new CreateRecord
                {
                    RecordType = "TXT",
                    Name = "",
                    Content = AutoFixture.Create<string>()
                }
            };

            // Act
            await _dnSimpleClient.Records.CreateRecordByDomainName(MockDomainName, request);

            // Assert
            var result = await _dnSimpleClient.Records.ListRecordsByDomainName(MockDomainName);
            result.Should().Contain(x => x.Record.Content == request.Record.Content);
        }

        [Fact]
        public async Task Can_get_record_by_domain_name()
        {
            await CreateDomain(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecord(content);

            // Act
            var result = await _dnSimpleClient.Records.GetRecordByDomainName(MockDomainName, record.Record.Id);

            // Assert
            result.Should().NotBeNull();
            result.Record.Content.Should().Be(content);
        }

        [Fact]
        public async Task Can_update_record_by_domain_name()
        {
            await CreateDomain(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecord(content);
            var updateRequest = new CreateRecordRequest
            {
                Record = new CreateRecord
                {
                    Name = "",
                    Content = AutoFixture.Create<string>()
                }
            };

            // Act
            await _dnSimpleClient.Records.UpdateRecordByDomainName(MockDomainName, record.Record.Id, updateRequest);

            // Assert
            var result = await _dnSimpleClient.Records.GetRecordByDomainName(MockDomainName, record.Record.Id);
            result.Record.Content.Should().Be(updateRequest.Record.Content);
        }

        [Fact]
        public async Task Can_delete_by_domain_name()
        {
            await CreateDomain(MockDomainName);

            var content = AutoFixture.Create<string>();
            var record = await CreateRecord(content);

            // Act
            await _dnSimpleClient.Records.DeleteRecordByDomainName(MockDomainName, record.Record.Id);

            // Assert
            var result = await _dnSimpleClient.Records.ListRecordsByDomainName(MockDomainName);
            result.Should().NotContain(x => x.Record.Id == record.Record.Id);
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

        private async Task<RecordResult> CreateRecord(string context)
        {
            var request = new CreateRecordRequest
            {
                Record = new CreateRecord
                {
                    RecordType = "TXT",
                    Name = "",
                    Content = context
                }
            };
            return await _dnSimpleClient.Records.CreateRecordByDomainName(MockDomainName, request);
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