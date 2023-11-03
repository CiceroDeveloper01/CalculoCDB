using CalculoCDBDomain.DTO;
using CalculoCDBDomain.OutPutDefault;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CalculoCDBTestWeb;

[Collection(nameof(FixturesEfetuarCalculoTest))]
public class FixturesEfetuarCalculoTest
{
    private HttpClient _httpClient;
    public FixturesEfetuarCalculoTest()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [Fact(DisplayName = "Efetuar o Calculo da API")]
    public async Task EfetuaCalculoExito()
    {
        var requisicao = await _httpClient.PostAsJsonAsync($"v1/Calcular/", new ValorInicialAplicaoDTO { PrazoInvestimento = 5, ValorInicial = 15.5 });
        var resposta = await requisicao.Content.ReadAsStringAsync();
        var comandoResulte = JsonConvert.DeserializeObject<CommandResult>(resposta);
        var resultadoInvestimentoDTO = JsonConvert.DeserializeObject<ResultadoInvestimentoDTO>(comandoResulte.Data.ToString());
        Assert.True(requisicao.IsSuccessStatusCode);
        Assert.Equal(16.268087186280276, resultadoInvestimentoDTO.ValorBruto);
        Assert.Equal(16.231483990111144, resultadoInvestimentoDTO.ValorLiquido);
        Assert.Equal(5, resultadoInvestimentoDTO.PrazoInvestimento);
    }

    [Fact(DisplayName = "Efetuar o Calculo da API Sem Exito")]
    public async Task EfetuaCalculoSemExito()
    {
        var requisicao = await _httpClient.PostAsJsonAsync($"v1/Calcular/", new ValorInicialAplicaoDTO { PrazoInvestimento = 0, ValorInicial = 0 });
        var resposta = await requisicao.Content.ReadAsStringAsync();
        var comandoResulte = JsonConvert.DeserializeObject<CommandResult>(resposta);
        Assert.False(requisicao.IsSuccessStatusCode);
        Assert.False(comandoResulte.Success);
    }
}