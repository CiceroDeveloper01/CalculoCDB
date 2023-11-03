using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Inferfaces;
using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBDomain.Taxas;
using CalculoCDBService.Taxas;
using Microsoft.Extensions.Logging;
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
        var mockloggerTaxa = new Mock<ILogger<TaxaOperacionaisService>>();
        var mocktaxaOperacionaisService = new TaxaOperacionaisService(mockTaxasOperacionaisRepository.Object, mockloggerTaxa.Object);
        
        Mock<IRepositoryBase<ImpostosOperacionais>> mockImpostosOperacionaisRepository = DataFixtures.MockImpostosOperacionaisRepository();
        var mockloggerImposto = new Mock<ILogger<ImpostosOperacionaisService>>();
        var mockimpostosOperacionaisService = new ImpostosOperacionaisService(mockImpostosOperacionaisRepository.Object, mockloggerImposto.Object);


        var mockloggerCalculo = new Mock<ILogger<EfetuarCalculoService>>();
        _efetuarCalculoService = new EfetuarCalculoService(mockimpostosOperacionaisService,
                                                           mocktaxaOperacionaisService,
                                                           mockloggerCalculo.Object);
    }
    [TestMethod]
    public void TestEfetuarCalculoInvalido()
    {
        var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDto { ValorInicial = 15.5, PrazoInvestimento = 0}).Result;
        Assert.AreEqual("Por favor, corrija os campos abaixo".ToString(), commandResult.Message.ToString());
    }
    [TestMethod]
    public void TestEfetuarCalculoValido()
    {
        var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDto { ValorInicial = 15.5, PrazoInvestimento = 5 }).Result;
        var resultadoInvestimentoDTO = (ResultadoInvestimentoDto)commandResult.Data;
        Assert.AreEqual(16.268087186280276, resultadoInvestimentoDTO.ValorBruto); 
        Assert.AreEqual(16.231483990111144, resultadoInvestimentoDTO.ValorLiquido);
        Assert.AreEqual(5, resultadoInvestimentoDTO.PrazoInvestimento);
    }
}