using Deviot.Administration.Api;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Test.BDD.Base;
using Deviot.Administration.Test.BDD.Fixtures;
using Deviot.Administration.Test.Common.Fakes.ViewModels;
using Deviot.Common;
using Deviot.Common.Models;
using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Administration.Test.BDD
{
    [Binding]
    [Scope(Feature = "Atualizar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class AtualizarUsuarioStepDefinitions : IntegrationTestBase
    {
        private UserViewModel _user;
        private GenericActionResult<object> _result;
        private HttpResponseMessage _httpResponseMessage;

        public AtualizarUsuarioStepDefinitions(
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
            var userFake = UserViewModelFake.GetUser1();
            _user = new UserViewModel
            {
                Id = userFake.Id,
                FullName = userFake.FullName,
                Email = userFake.Email
            };
        }

        [Given(@"que tenho um usuário com email já utilizado")]
        public void GivenQueTenhoUmUsuarioComEmailJaExistente()
        {
            var userFake1 = UserViewModelFake.GetUser1();
            var userFake2 = UserViewModelFake.GetUser2();
            _user = new UserViewModel
            {
                Id = userFake1.Id,
                FullName = userFake1.FullName,
                Email = userFake2.Email
            };
        }

        [Given(@"que que tenho um usuário com id inexistente")]
        public void GivenQueQueTenhoUmUsuarioComIdInvalido()
        {
            _user = new UserViewModel
            {
                Id = Guid.NewGuid().ToString(),
                FullName = "Teste",
                Email = "Email@teste.com"
            };
        }

        [Given(@"que tenho um usuário com nome inválido")]
        public void GivenQueTenhoUmUsuarioComNomeInvalido()
        {
            _user = new UserViewModel
            {
                Id = Guid.NewGuid().ToString(),
                FullName = string.Empty,
                Email = "Email@teste.com"
            };
        }

        [When(@"executar a url via PUT")]
        public async Task WhenExecutarAUrlViaPUT()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_user));
            _httpResponseMessage = await _integrationTestFixture.Client.PutAsync($"/api/v1/user/{_user.Id}", content);
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
