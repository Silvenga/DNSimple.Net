namespace DNSimple.Net.V1.Client.Options
{
    using CommandLine;

    [Verb("down", HelpText = "Pull zone from DNSimple")]
    public class DownOptions
    {
        [Value(0, Required = true, HelpText = "Zone config file")]
        public string ConfigFile { get; set; }

        [Value(0, Required = true, HelpText = "Input file")]
        public string InputFile { get; set; }
    }
}