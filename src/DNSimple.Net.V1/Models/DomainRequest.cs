namespace DNSimple.Net.V1.Models
{
    public class DomainRequest
    {
        public DomainRequest(string domainName)
        {
            Domain = new DomainName
            {
                Name = domainName
            };
        }

        public DomainName Domain { get; set; }
    }
}