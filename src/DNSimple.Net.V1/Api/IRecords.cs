namespace DNSimple.Net.V1.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Models;

    using Refit;

    public interface IRecords
    {
        [Get("/v1/domains/{domainName}/records")]
        Task<IList<RecordResponse>> ListRecordsByDomainNameAsync(string domainName);

        [Get("/v1/domains/{domainId}/records")]
        Task<IList<RecordResponse>> ListRecordsByDomainIdAsync(int domainId);

        [Post("/v1/domains/{domainName}/records")]
        Task<RecordResponse> CreateRecordByDomainNameAsync(string domainName, RecordRequest request);

        [Post("/v1/domains/{domainId}/records")]
        Task<RecordResponse> CreateRecordByDomainIdAsync(int domainId, RecordRequest request);

        [Get("/v1/domains/{domainName}/records/{recordId}")]
        Task<RecordResponse> GetRecordByDomainNameAsync(string domainName, int recordId);

        [Get("/v1/domains/{domainId}/records/{recordId}")]
        Task<RecordResponse> GetRecordByDomainIdAsync(int domainId, int recordId);

        [Put("/v1/domains/{domainName}/records/{recordId}")]
        Task<RecordResponse> UpdateRecordByDomainNameAsync(string domainName, int recordId, RecordRequest request);

        [Put("/v1/domains/{domainId}/records/{recordId}")]
        Task<RecordResponse> UpdateRecordByDomainIdAsync(int domainId, int recordId, RecordRequest request);

        [Delete("/v1/domains/{domainName}/records/{recordId}")]
        Task DeleteRecordByDomainNameAsync(string domainName, int recordId);

        [Delete("/v1/domains/{domainId}/records/{recordId}")]
        Task DeleteRecordByDomainIdAsync(int domainId, int recordId);
    }
}