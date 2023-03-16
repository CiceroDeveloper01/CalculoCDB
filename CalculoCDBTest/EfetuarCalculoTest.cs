using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Taxas;
using CalculoCDBService;
using CalculoCDBService.Inferfaces;
using CalculoCDBService.Inferfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoCDBTest
{
    [TestClass]
    public class EfetuarCalculoTest
    {
        IEfetuarCalculoService _efetuarCalculoService;
        ITaxaOperacionaisService _taxaOperacionaisService;
        IImpostoOperacionaisService _impostoOperacionaisService;

        public EfetuarCalculoTest()
        {

            Mock<IRepositoryBase<TaxasOperacionais>> mockTaxasOperacionaisRepository = DataFixtures.MockTaxasOperacionaisRepository();
            var mockimpostosOperacionaisService = new Mock<IImpostoOperacionaisService>();

            var mocktaxaOperacionaisService = new TaxaOperacionaisService(mockTaxasOperacionaisRepository.Object);

            _efetuarCalculoService = new EfetuarCalculoService(mockimpostosOperacionaisService.Object, mocktaxaOperacionaisService);
        }

        [TestMethod]
        public void TestEfetuarCalculoInvalido()
        {
            var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDTO { ValorInicial = 15.5, PrazoInvestimento = 0}).Result;
            Assert.Equals("Por favor, corrija os campos abaixo".ToString(), commandResult.Message.ToString());
        }

        [TestMethod]
        public void TestEfetuarCalculoValido()
        {
            var commandResult = _efetuarCalculoService.EfetuarCalculo(new ValorInicialAplicaoDTO { ValorInicial = 15.5, PrazoInvestimento = 5 }).Result;
            Assert.Equals("Por favor, corrija os campos abaixo".ToString(), commandResult.Message.ToString());
        }

    }
}
