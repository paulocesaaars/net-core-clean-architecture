using Deviot.Administration.Api;
using Deviot.Administration.Test.BDD.Fixtures;
using Xunit.Abstractions;

namespace Deviot.Administration.Test.BDD.Base
{
    public abstract class IntegrationTestBase
    {
        protected readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        protected readonly ITestOutputHelper _testOutputHelper;

        protected IntegrationTestBase(
            IntegrationTestFixture<Startup> integrationTestFixture,
            ITestOutputHelper testOutputHelper
            )
        {
            _integrationTestFixture = integrationTestFixture;
            _testOutputHelper = testOutputHelper;
        }
    }
}
