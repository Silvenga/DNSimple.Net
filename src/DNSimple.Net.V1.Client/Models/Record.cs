namespace DNSimple.Net.V1.Client.Models
{
    public class Record
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string RecordType { get; set; }
        public int Ttl { get; set; }
        public int? Priority { get; set; }
    }
}