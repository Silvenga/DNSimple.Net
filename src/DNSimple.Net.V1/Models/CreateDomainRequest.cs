namespace DNSimple.Net.V1.Models
{
    public class CreateDomainRequest
    {
        public CreateDomain Domain { get; set; }
    }

    public class CreateDomain
    {
        public string Name { get; set; }
    }
}