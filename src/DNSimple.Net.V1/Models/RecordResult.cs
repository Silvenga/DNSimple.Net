namespace DNSimple.Net.V1.Models
{
    using System;

    public class RecordResult
    {
        public Record Record { get; set; }
    }

    public class Record
    {
        public string Name { get; set; }
        public int Ttl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PdnsIdentifier { get; set; }
        public int DomainId { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public string RecordType { get; set; }
        public int? Prio { get; set; }
    }
}