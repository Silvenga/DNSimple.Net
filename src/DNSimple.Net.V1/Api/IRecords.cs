namespace DNSimple.Net.V1.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Models;

    using Refit;

    public interface IRecords
    {
        [Get("/v1/domains/{domainName}/records")]
        Task<IList<RecordResult>> ListRecordsByName(string domainName);

        [Get("/v1/domains/{domainId}/records")]
        Task<IList<RecordResult>> ListRecordsById(int domainId);
    }
}