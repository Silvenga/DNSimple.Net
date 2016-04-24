namespace DNSimple.Net.V1.Client
{
    using System.Collections.Generic;

    using CommandLine;

    using DNSimple.Net.V1.Client.Actions;
    using DNSimple.Net.V1.Client.Helpers;
    using DNSimple.Net.V1.Client.Options;

    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationParser parser = new ConfigurationParser();

            Parser.Default.ParseArguments<UpOptions, DownOptions, NewOptions>(args)
                  .WithParsed<UpOptions>(x => new UpAction())
                  .WithParsed<DownOptions>(x => new DownAction(parser).Run(x))
                  .WithParsed<NewOptions>(x => new NewAction(parser).Run(x))
                  .WithNotParsed(Error);
        }

        private static void Error(IEnumerable<Error> opts)
        {
        }
    }
}