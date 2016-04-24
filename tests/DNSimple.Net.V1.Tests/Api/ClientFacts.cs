namespace DNSimple.Net.V1.Tests.Api
{
    using System;

    using DNSimple.Net.V1.Api;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class ClientFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();

        [Fact]
        public void When_creating_with_email_token()
        {
            var email = AutoFixture.Create<string>();
            var token = AutoFixture.Create<string>();

            // Act
            Action action = () => new DNSimpleClient(email, token);

            // Assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void When_creating_with_email_password()
        {
            var email = AutoFixture.Create<string>();
            var token = AutoFixture.Create<string>();

            // Act
            Action action = () => new DNSimpleClient(email, token, true);

            // Assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void When_creating_with_domain_token()
        {
            var token = AutoFixture.Create<string>();

            // Act
            Action action = () => new DNSimpleClient(token);

            // Assert
            action.ShouldNotThrow();
        }
    }
}