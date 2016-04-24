namespace DNSimple.Net.V1.Client.Actions
{
    using DNSimple.Net.V1.Client.Helpers;
    using DNSimple.Net.V1.Client.Models;
    using DNSimple.Net.V1.Client.Options;

    public class NewAction
    {
        private readonly IConfigurationParser _parser;

        public NewAction(IConfigurationParser parser)
        {
            _parser = parser;
        }

        public void Run(NewOptions options)
        {
            var domainConfigFile = $"{options.Domain}.auth.yml";
            var zoneConfigFile = $"{options.Domain}.zone.yml";

            var domainConfig = new DomainConfiguration
            {
                ZoneOrigin = options.Domain,
                DomainToken = "domain-token-here"
            };
            var zoneConfig = new ZoneConfiguration
            {
                ZoneOrigin = options.Domain
            };

            _parser.CreateConfiguration(domainConfigFile, domainConfig);
            _parser.CreateConfiguration(zoneConfigFile, zoneConfig);
        }
    }
}