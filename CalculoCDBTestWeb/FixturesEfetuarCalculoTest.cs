using CalculoCDBDomain.DTO;
using CalculoCDBDomain.OutPutDefault;
using CalculoCDBPresentation;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CalculoCDBTestWeb
{
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class FixturesEfetuarCalculoTest
    {
        private readonly IntegrationTestFixture<StartupApiTest> _integrationTestFixture;
        public FixturesEfetuarCalculoTest(IntegrationTestFixture<StartupApiTest> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Fact(DisplayName = "Efetuar o Calculo da API")]
        public async Task EfetuaCalculoExito()
        {
            var requisicao = await _integrationTestFixture.Client.PostAsJsonAsync($"/EfetuarCalculo/", new ValorInicialAplicaoDTO { PrazoInvestimento = 5, ValorInicial = 15.5 });
            var resposta = await requisicao.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(resposta);
            var resultadoInvestimentoDTO = JsonConvert.DeserializeObject<ResultadoInvestimentoDTO>(commandResult.Data.ToString());
            Assert.True(requisicao.IsSuccessStatusCode);
            Assert.Equal(16.268087186280276, resultadoInvestimentoDTO.ValorBruto);
            Assert.Equal(16.231483990111144, resultadoInvestimentoDTO.ValorLiquido);
            Assert.Equal(5, resultadoInvestimentoDTO.PrazoInvestimento);
        }

        [Fact(DisplayName = "Efetuar o Calculo da API Sem Exito")]
        public async Task EfetuaCalculoSemExito()
        {
            var requisicao = await _integrationTestFixture.Client.PostAsJsonAsync($"/EfetuarCalculo/", new ValorInicialAplicaoDTO { PrazoInvestimento = 0, ValorInicial = 0 });
            var resposta = await requisicao.Content.ReadAsStringAsync();
            var commandResult = JsonConvert.DeserializeObject<CommandResult>(resposta);
            Assert.False(requisicao.IsSuccessStatusCode);
            Assert.Equal(false, commandResult.Success);
        }

    }
}