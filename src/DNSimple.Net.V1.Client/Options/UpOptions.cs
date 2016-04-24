namespace DNSimple.Net.V1.Client.Options
{
    using CommandLine;

    [Verb("up", HelpText = "Push zone to DNSimple")]
    public class UpOptions
    {
        [Value(0, Required = true, HelpText = "Input file")]
        public string InputFile { get; set; }
    }
}