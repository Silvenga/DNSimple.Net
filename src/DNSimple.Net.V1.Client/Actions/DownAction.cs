namespace DNSimple.Net.V1.Client.Actions
{
    using DNSimple.Net.V1.Client.Helpers;
    using DNSimple.Net.V1.Client.Options;

    public class DownAction
    {
        private readonly IConfigurationParser _parser;

        public DownAction(IConfigurationParser parser)
        {
            _parser = parser;
        }

        public void Run(DownOptions options)
        {

        }
    }
}