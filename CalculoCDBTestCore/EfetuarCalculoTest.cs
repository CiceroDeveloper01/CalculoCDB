using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Taxas;
using CalculoCDBService;
using CalculoCDBService.Inferfaces;
using CalculoCDBService.Inferfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CalculoCDBTest;

[TestClass]
public class EfetuarCalculoTest
{
    IEfetuarCalculoService _efetuarCalculoService;
    public EfetuarCalculoTest()
    {
        Mock<IRepositoryBase<TaxasOperacionais>> mockTaxasOperacionaisRepository = DataFixtures.MockTaxasOperacionaisRepository();
        var mocktaxaOperacionaisService = new TaxaOperacionaisService(mockTaxasOperacionaisRepository.Object);
        Mock<IRepositoryBase<ImpostosOperacionais>> mockImpostosOperacionaisRepository = DataFixtures.MockImpostosOperacionaisRepository();
        var mockimpostosOperacionaisService = new ImpostosOperacionaisService(mockImpostosOperacionaisRepository.Object);
        _efetuarCalculoService = new EfetuarCalculoService(mockimpostosOperacionaisService, mocktaxaOperacionaisService);
    }
    [TestMethod]
    public void TestEfetuarCalculoInvalido()
    {
        var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDTO { ValorInicial = 15.5, PrazoInvestimento = 0}).Result;
        Assert.AreEqual("Por favor, corrija os campos abaixo".ToString(), commandResult.Message.ToString());
    }
    [TestMethod]
    public void TestEfetuarCalculoValido()
    {
        var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDTO { ValorInicial = 15.5, PrazoInvestimento = 5 }).Result;
        var resultadoInvestimentoDTO = (ResultadoInvestimentoDTO)commandResult.Data;
        Assert.AreEqual(16.268087186280276, resultadoInvestimentoDTO.ValorBruto); 
        Assert.AreEqual(16.231483990111144, resultadoInvestimentoDTO.ValorLiquido);
        Assert.AreEqual(5, resultadoInvestimentoDTO.PrazoInvestimento);
    }
}