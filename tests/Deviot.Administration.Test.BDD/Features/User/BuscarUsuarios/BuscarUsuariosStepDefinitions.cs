using Deviot.Administration.Api;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Test.BDD.Base;
using Deviot.Administration.Test.BDD.Fixtures;
using Deviot.Common;
using Deviot.Common.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Administration.Test.BDD
{
    [Binding]
    [Scope(Feature = "Buscar usuários")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class BuscarUsuariosStepDefinitions : IntegrationTestBase
    {
        private string _url;
        private HttpResponseMessage _httpResponseMessage;
        private GenericActionResult<IEnumerable<UserViewModel>> _result;

        public BuscarUsuariosStepDefinitions(
            IntegrationTestFixture<Startup> integrationTestFixture, 
            ITestOutputHelper testOutputHelper
            ) : base(integrationTestFixture, testOutputHelper)
        {
        }

        [Given(@"que sou um consumidor de api")]
        public void GivenQueSouUmConsumidorDeApi()
        {
            return;
        }

        [Given(@"que tenho parâmetros de pesquisa '([^']*)' com valor '([^']*)'")]
        public void GivenQueTenhoParametrosDePesquisaComValor(string key, string value)
        {
            _url = string.Format("/api/v1/user?{0}={1}", key, value);
        }

        [When(@"executar a url via GET")]
        public async Task WhenExecutarAUrlViaGET()
        {
            _httpResponseMessage = await _integrationTestFixture.Client.GetAsync(_url);
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(json))
                _result = Utils.Deserializer<GenericActionResult<IEnumerable<UserViewModel>>>(json);
        }

        [Then(@"a api retornará status code (.*)")]
        public void ThenAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }

        [Then(@"todos usuários encontrados")]
        public void ThenTodosUsuariosEncontrados()
        {
            _result.Data.Should().NotBeEmpty();
        }

        [Then(@"a mensagem '([^']*)'")]
        public void ThenAMensagem(string p0)
        {
            _result.Messages.Should().Contain(p0);
        }
    }
}
