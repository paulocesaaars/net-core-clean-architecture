using Deviot.Administration.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Deviot.Administration.Test.BDD.Fixtures
{
    [CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]
    public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestFixture<Startup>>
    {
    }

    public class IntegrationTestFixture<TStartup> : IDisposable where TStartup : class
    {
        private const string TOKEN_ERROR = "O token não foi informado";
        private const string MEDIA_TYPE = "application/json";
        private const string ENVIRONMENT = "Testing";
        private const string TOKEN_SCHEME = "Bearer";

        public TestServer Server { get; private set; }

        public HttpClient Client { get; private set; }

        public IntegrationTestFixture()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>()
                                              .UseEnvironment(ENVIRONMENT);

            Server = new TestServer(builder)
            {
                BaseAddress = new Uri("http://localhost"),
            };

            Client = Server.CreateClient();
        }

        public void AddToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException(TOKEN_ERROR);

            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TOKEN_SCHEME, token);
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
