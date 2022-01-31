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
    [Scope(Feature = "Buscar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class BuscarUsuarioStepDefinitions : IntegrationTestBase
    {
        private string _id;
        private UserViewModel _user;
        private GenericActionResult<UserViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public BuscarUsuarioStepDefinitions(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                  ITestOutputHelper testOutputHelper
                                 ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {
            _user = UserViewModelFake.GetUser1();
        }

        [Given(@"que sou um consumidor de api")]
        public void GivenQueSouUmConsumidorDeApi()
        {
            return;
        }

        [Given(@"que tenho um id de usuário válido")]
        public void GivenQueTenhoUmIdDeUsuarioValido()
        {
            _id = _user.Id;
        }

        [Given(@"que que tenho um id de usuário inválido")]
        public void GivenQueQueTenhoUmIdDeUsuarioInvalido()
        {
            _id = Guid.NewGuid().ToString();    
        }


        [When(@"executar a url via GET")]
        public async Task WhenExecutarAUrlViaGET()
        {
            var url = $"/api/v1/user/{_id}";
            _httpResponseMessage = await _integrationTestFixture.Client.GetAsync(url);
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(json))
                _result = Utils.Deserializer<GenericActionResult<UserViewModel>>(json);
        }

        [Then(@"a api retornará status code (.*)")]
        public void ThenAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }

        [Then(@"o usuário desejado")]
        public void ThenOUsuarioDesejado()
        {
            _result.Data.Should().BeEquivalentTo(_user);
        }

        [Then(@"a mensagem '([^']*)'")]
        public void ThenAMensagem(string p0)
        {
            _result.Messages.Should().Contain(p0);
        }
    }
}
