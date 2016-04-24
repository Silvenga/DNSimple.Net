namespace DNSimple.Net.V1.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Models;

    using Refit;

    public interface IDomains
    {
        [Get("/v1/domains")]
        Task<IList<DomainResult>> ListDomainsAsync();

        [Post("/v1/domains")]
        Task<DomainResult> CreateDomainAsync(DomainRequest request);

        [Get("/v1/domains/{domainName}")]
        Task<DomainResult> GetDomainAsync(string domainName);

        [Delete("/v1/domains/{domainName}")]
        Task DeleteDomainAsync(string domainName);
    }
}