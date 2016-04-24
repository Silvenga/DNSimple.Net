namespace DNSimple.Net.V1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using DNSimple.Net.Common.Json;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RecordResponse
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

        [JsonProperty("prio")]
        public int? Priority { get; set; }

        private const string NestedProperty = "record";

#pragma warning disable 649
        // ReSharper disable once CollectionNeverUpdated.Local
        [JsonExtensionData] private IDictionary<string, JToken> _additionalData;
#pragma warning restore 649

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_additionalData?.ContainsKey(NestedProperty) == true && _additionalData?.Count == 1)
            {
                var nestedObject = _additionalData[NestedProperty].ToObject<RecordResponse>(new JsonSerializer
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver()
                });

                nestedObject.CopyPropertiesTo(this);
            }
        }
    }
}