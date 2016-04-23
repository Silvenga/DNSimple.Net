namespace DNSimple.Net.V1.Models
{
    public class CreateRecordRequest
    {
        public CreateRecord Record { get; set; }
    }

    public class CreateRecord
    {
        public string Name { get; set; }
        public string RecordType { get; set; }
        public string Content { get; set; }
        public int? Ttl { get; set; }
        public int? Prio { get; set; }
    }
}