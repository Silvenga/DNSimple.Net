namespace DNSimple.V1.Models
{
    public class ListDomainResult
    {
        public ListDomain Domain { get; set; }
    }

    public class ListDomain
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RegistrantId { get; set; }
        public string Name { get; set; }
        public string UnicodeName { get; set; }
        public string Token { get; set; }
        public string State { get; set; }
        public object Language { get; set; }
        public bool Lockable { get; set; }
        public bool AutoRenew { get; set; }
        public bool WhoisProtected { get; set; }
        public int? RecordCount { get; set; }
        public int? ServiceCount { get; set; }
        public string ExpiresOn { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}