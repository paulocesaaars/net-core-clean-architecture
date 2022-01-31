using Deviot.Administration.Api;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Test.BDD.Base;
using Deviot.Administration.Test.BDD.Fixtures;
using Deviot.Administration.Test.Common.Fakes.ViewModels;
using Deviot.Common;
using Deviot.Common.Models;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Administration.Test.BDD
{
    [Binding]
    [Scope(Feature = "Adicionar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class AdicionarUsuarioStepDefinitions : IntegrationTestBase
    {
        private NewUserViewModel _user;
        private GenericActionResult<object> _result;
        private HttpResponseMessage _httpResponseMessage;

        public AdicionarUsuarioStepDefinitions(
            IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
            ITestOutputHelper testOutputHelper
            ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {

        }

        [Given(@"que sou um consumidor de api")]
        public void GivenQueSouUmConsumidorDeApi()
        {
            return;
        }

        [Given(@"que tenho um usuário válido")]
        public void GivenQueTenhoUmUsuarioValido()
        {
            var name = Utils.GenerateRandomString(5);
            _user = new NewUserViewModel { 
                FullName = name,
                Email = $"{name}@teste.com"
            };
        }

        [Given(@"que tenho um usuário com nome inválido")]
        public void GivenQueTenhoUmUsuarioComNomeInvalido()
        {
            _user = new NewUserViewModel
            {
                FullName = string.Empty,
                Email = string.Empty
            };
        }

        [Given(@"que tenho um usuário com email já utilizado")]
        public void GivenQueTenhoUmUsuarioComEmailJaExistente()
        {
            var userFake = UserViewModelFake.GetUser1();
            _user = new NewUserViewModel
            {
                FullName = userFake.FullName,
                Email = userFake.Email
            };
        }

        [When(@"executar a url via POST")]
        public async Task WhenExecutarAUrlViaPOSTAsync()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_user));
            _httpResponseMessage = await _integrationTestFixture.Client.PostAsync($"/api/v1/user", content);
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(json))
                _result = Utils.Deserializer<GenericActionResult<object>>(json);
        }

        [Then(@"a api retornará status code (.*)")]
        public void ThenAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
            
        }

        [Then(@"a mensagem '([^']*)'")]
        public void ThenAMensagem(string p0)
        {
            _result.Messages.Should().Contain(p0);
        }
    }
}
