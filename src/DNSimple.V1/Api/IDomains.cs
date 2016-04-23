namespace DNSimple.V1.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DNSimple.V1.Models;

    using Refit;

    public interface IDomains
    {
        [Get("/v1/domains")]
        Task<IList<ListDomainResult>> ListDomains();

        [Post("/v1/domains")]
        Task<ListDomainResult> CreateDomain(CreateDomainRequest request);

        [Delete("/v1/domains/{domainName}")]
        Task DeleteDomain(string domainName);
    }
}