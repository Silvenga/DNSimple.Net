namespace DNSimple.Net.V1.Client.Tests.Client
{
    using System.IO;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class ClientFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void New_command_creates_config_files()
        {
            var domain = AutoFixture.Create<string>();

            // Act
            Program.Main(new[] {"new", domain});

            // Assert
            var authFile = $"{domain}.auth.yml";
            var zoneFile = $"{domain}.zone.yml";

            File.Exists(authFile).Should().BeTrue();
            File.Exists(zoneFile).Should().BeTrue();

            var auth = File.ReadAllText(authFile);
            var zone = File.ReadAllText(zoneFile);

            auth.Should().NotBeNullOrWhiteSpace();
            zone.Should().NotBeNullOrWhiteSpace();
        }
    }
}