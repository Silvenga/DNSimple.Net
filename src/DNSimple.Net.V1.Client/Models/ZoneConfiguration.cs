namespace DNSimple.Net.V1.Client.Models
{
    using System.Collections.Generic;

    public class ZoneConfiguration
    {
        public string ZoneOrigin { get; set; }

        public IList<Record> Records { get; set; }
    }
}