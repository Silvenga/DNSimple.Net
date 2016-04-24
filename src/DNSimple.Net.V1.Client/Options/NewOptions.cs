namespace DNSimple.Net.V1.Client.Options
{
    using CommandLine;

    [Verb("new", HelpText = "Create configuration files")]
    public class NewOptions
    {
        [Value(0, Required = true, HelpText = "Domain")]
        public string Domain { get; set; }
    }
}