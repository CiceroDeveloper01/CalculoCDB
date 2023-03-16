using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Taxas;
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
    public class ImpostosOperacionaisTest
    {
        public readonly IImpostosOperacionaisRepository _mockImpostosOperacionaisRepository;

        public ImpostosOperacionaisTest()
        {
            IList<ImpostosOperacionais> impostosOperacionais = new List<ImpostosOperacionais>();
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 1, TempoCalculado = EImpostosTempoInvestimento.AteSeisMeses.ToString("G"), PrazoInicialCalculo = 1, PrazoFinalCalculo = 6, ValorImposto = 22.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now }); ;
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 2, TempoCalculado = EImpostosTempoInvestimento.AteDozeMeses.ToString("G"), PrazoInicialCalculo = 7, PrazoFinalCalculo = 12, ValorImposto = 22.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 3, TempoCalculado = EImpostosTempoInvestimento.AteVinteQuatroMeses.ToString("G"), PrazoInicialCalculo = 13, PrazoFinalCalculo = 24, ValorImposto = 17.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 4, TempoCalculado = EImpostosTempoInvestimento.Acima24Meses.ToString("G"), PrazoInicialCalculo = 25, PrazoFinalCalculo = 1000, ValorImposto = 15.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });

            Mock<IImpostosOperacionaisRepository> mockImpostosOperacionaisRepository = new Mock<IImpostosOperacionaisRepository>();

            mockImpostosOperacionaisRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<ImpostosOperacionais>)impostosOperacionais));

            mockImpostosOperacionaisRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => Task.FromResult(impostosOperacionais.Where(x => x.ID == i).Single()));

            mockImpostosOperacionaisRepository.Setup(mr => mr.Add(It.IsAny<ImpostosOperacionais>())).Returns(
                (ImpostosOperacionais target) =>
                {
                    DateTime now = DateTime.Now;
                    if (target.ID.Equals(default(int)))
                    {
                        target.DataCriacao = now;
                        target.DataAlteracao = now;
                        target.ID = impostosOperacionais.Count() + 1;
                        impostosOperacionais.Add(target);
                    }
                    else
                    {
                        var original = impostosOperacionais.Where(q => q.ID == target.ID).Single();
                        if (original == null)
                            return Task.FromResult(false);
                        original.PrazoInicialCalculo = target.PrazoInicialCalculo;
                        original.PrazoFinalCalculo = target.PrazoFinalCalculo;
                        original.TempoCalculado = target.TempoCalculado;
                        original.ValorImposto = target.ValorImposto;
                        original.DataAlteracao = now;
                    }
                    return Task.FromResult(true);
                });

            mockImpostosOperacionaisRepository.Setup(mr => mr.Remove(It.IsAny<ImpostosOperacionais>()));

            _mockImpostosOperacionaisRepository = mockImpostosOperacionaisRepository.Object;
        }
        [TestMethod]
        public void ReturnAllImpostos()
        {
            IList<ImpostosOperacionais> testimpostosOperacionais = (IList<ImpostosOperacionais>)_mockImpostosOperacionaisRepository.GetAll().Result;
            Assert.IsNotNull(testimpostosOperacionais); 
            Assert.AreEqual(4, testimpostosOperacionais.Count); 
        }
        [TestMethod]
        public void AddImpostos()
        {
            ImpostosOperacionais newImpostosOperacionais = new ImpostosOperacionais
            { TempoCalculado = EImpostosTempoInvestimento.Acima24Meses.ToString("G"), PrazoInicialCalculo = 36, PrazoFinalCalculo = 1000, ValorImposto = 12.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now };
            var impostosOperacionais = this._mockImpostosOperacionaisRepository.GetAll().Result;
            Assert.AreEqual(4, impostosOperacionais.Count());
            _mockImpostosOperacionaisRepository.Add(newImpostosOperacionais);
            impostosOperacionais = this._mockImpostosOperacionaisRepository.GetAll().Result;
            Assert.AreEqual(5, impostosOperacionais.Count());
            ImpostosOperacionais testImpostosOperacionais = this._mockImpostosOperacionaisRepository.GetById(5).Result;
            Assert.IsNotNull(testImpostosOperacionais); 
            Assert.IsInstanceOfType(testImpostosOperacionais, typeof(ImpostosOperacionais)); // Test type
            Assert.AreEqual(5, testImpostosOperacionais.ID);
        }
        [TestMethod]
        public void UpdateImpostos()
        {
            ImpostosOperacionais testImpostosOperacionais = this._mockImpostosOperacionaisRepository.GetById(1).Result;
            testImpostosOperacionais.PrazoFinalCalculo = 8;
            _mockImpostosOperacionaisRepository.Update(testImpostosOperacionais);
            var updatedImpostosOperacionais = _mockImpostosOperacionaisRepository.GetById(1).Result;
            Assert.AreEqual(8, updatedImpostosOperacionais.PrazoFinalCalculo);
        }
        [TestMethod]
        public void DeleteImpostos()
        {
            ImpostosOperacionais testImpostosOperacionais = this._mockImpostosOperacionaisRepository.GetById(1).Result;
            _mockImpostosOperacionaisRepository.Remove(testImpostosOperacionais);
            var deletedImpostosOperacionais = _mockImpostosOperacionaisRepository.GetById(1).Result;
            Assert.AreEqual(null, deletedImpostosOperacionais);
        }

    }
}
