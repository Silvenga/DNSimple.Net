namespace DNSimple.Net.V1.Api
{
    using System.Threading.Tasks;

    using DNSimple.Net.V1.Models;

    using Refit;

    public interface IZones
    {
        [Get("/v1/domains/{domainName}/zone")]
        Task<ExportResult> ExportByDomainNameAsync(string domainName);

        [Get("/v1/domains/{domainId}/zone")]
        Task<ExportResult> ExportByDomainIdAsync(int domainId);
    }
}