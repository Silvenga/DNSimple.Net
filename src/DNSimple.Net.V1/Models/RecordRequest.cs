namespace DNSimple.Net.V1.Models
{
    using Newtonsoft.Json;

    public class RecordRequest
    {
        public string Name { get; set; }
        public string RecordType { get; set; }
        public string Content { get; set; }
        public int? Ttl { get; set; }

        [JsonProperty("prio")]
        public int? Priorty { get; set; }
    }
}