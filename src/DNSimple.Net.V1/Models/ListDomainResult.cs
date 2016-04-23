namespace DNSimple.Net.V1.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using DNSimple.Net.Common.Json;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ListDomainResult
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
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

        private const string NestedProperty = "domain";

#pragma warning disable 649
        // ReSharper disable once CollectionNeverUpdated.Local
        [JsonExtensionData] private IDictionary<string, JToken> _additionalData;
#pragma warning restore 649

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_additionalData?.ContainsKey(NestedProperty) == true && _additionalData?.Count == 1)
            {
                var nestedObject = _additionalData[NestedProperty].ToObject<ListDomainResult>(new JsonSerializer
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver()
                });

                nestedObject.CopyPropertiesTo(this);
            }
        }
    }

    public class CreateDomainRequest
    {
        public CreateDomainRequest(string domainName)
        {
            Domain = new CreateDomain
            {
                Name = domainName
            };
        }

        public CreateDomain Domain { get; set; }
    }

    public class CreateDomain
    {
        public string Name { get; set; }
    }
}