namespace DNSimple.Net.V1.Client.Helpers
{
    using System.IO;

    using DNSimple.Net.V1.Client.Models;

    using YamlDotNet.Serialization;

    public interface IConfigurationParser
    {
        ZoneConfiguration ParseZoneConfiguration(string file);
        DomainConfiguration ParseDomainConfiguration(string file);
        void CreateConfiguration(string file, object o);
    }

    public class ConfigurationParser : IConfigurationParser
    {
        public ZoneConfiguration ParseZoneConfiguration(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var reader = new StreamReader(stream);
                var deserializer = new Deserializer();
                return deserializer.Deserialize<ZoneConfiguration>(reader);
            }
        }

        public DomainConfiguration ParseDomainConfiguration(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var reader = new StreamReader(stream);
                var deserializer = new Deserializer();
                return deserializer.Deserialize<DomainConfiguration>(reader);
            }
        }

        public void CreateConfiguration(string file, object o)
        {
            using (var stream = File.OpenWrite(file))
            {
                var writer = new StreamWriter(stream)
                {
                    AutoFlush = true
                };
                var serializer = new Serializer();
                serializer.Serialize(writer, o);
            }
        }
    }
}