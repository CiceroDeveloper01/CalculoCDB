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
        public readonly IRepositoryBase<ImpostosOperacionais> _mockImpostosOperacionaisRepository;

        public ImpostosOperacionaisTest()
        {
            Mock<IRepositoryBase<ImpostosOperacionais>> mockImpostosOperacionaisRepository = DataFixtures.MockImpostosOperacionaisRepository();
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
            ImpostosOperacionais testImpostosOperacionais = this._mockImpostosOperacionaisRepository.GetById(3).Result;
            testImpostosOperacionais.PrazoFinalCalculo = 8;
            _mockImpostosOperacionaisRepository.Update(testImpostosOperacionais);
            var updatedImpostosOperacionais = _mockImpostosOperacionaisRepository.GetById(3).Result;
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
