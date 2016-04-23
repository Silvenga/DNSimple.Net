namespace DNSimple.Net.V1.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Models;

    using Refit;

    public interface IRecords
    {
        [Get("/v1/domains/{domainName}/records")]
        Task<IList<RecordResult>> ListRecordsByDomainName(string domainName);

        [Get("/v1/domains/{domainId}/records")]
        Task<IList<RecordResult>> ListRecordsByDomainId(int domainId);

        [Post("/v1/domains/{domainName}/records")]
        Task<RecordResult> CreateRecordByDomainName(string domainName, CreateRecordRequest request);

        [Post("/v1/domains/{domainId}/records")]
        Task<RecordResult> CreateRecordByDomainId(int domainId, CreateRecordRequest request);

        [Get("/v1/domains/{domainName}/records/{recordId}")]
        Task<RecordResult> GetRecordByDomainName(string domainName, int recordId);

        [Get("/v1/domains/{domainId}/records/{recordId}")]
        Task<RecordResult> GetRecordByDomainId(int domainId, int recordId);

        [Put("/v1/domains/{domainName}/records/{recordId}")]
        Task<RecordResult> UpdateRecordByDomainName(string domainName, int recordId, CreateRecordRequest request);

        [Put("/v1/domains/{domainId}/records/{recordId}")]
        Task<RecordResult> UpdateRecordByDomainId(int domainId, int recordId, CreateRecordRequest request);

        [Delete("/v1/domains/{domainName}/records/{recordId}")]
        Task DeleteRecordByDomainName(string domainName, int recordId);

        [Delete("/v1/domains/{domainId}/records/{recordId}")]
        Task DeleteRecordByDomainId(int domainId, int recordId);
    }
}