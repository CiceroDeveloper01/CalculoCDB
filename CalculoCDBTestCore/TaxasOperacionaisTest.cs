using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBDomain.Taxas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CalculoCDBTest;

[TestClass]
public class TaxasOperacionaisTest
{
    public readonly IRepositoryBase<TaxasOperacionais> _mockTaxasOperacionaisRepository;

    public TaxasOperacionaisTest()
    {
        Mock<IRepositoryBase<TaxasOperacionais>> mockTaxasOperacionaisRepository = DataFixtures.MockTaxasOperacionaisRepository();
        _mockTaxasOperacionaisRepository = mockTaxasOperacionaisRepository.Object;
    }
    [TestMethod]
    public void ReturnAllTaxasOperacionais()
    {
        IList<TaxasOperacionais> testtaxasOperacionais = (IList<TaxasOperacionais>)_mockTaxasOperacionaisRepository.GetAll().Result;
        Assert.IsNotNull(testtaxasOperacionais);
        Assert.AreEqual(2, testtaxasOperacionais.Count);
    }
    [TestMethod]
    public void AddTaxasOperacionais()
    {
        TaxasOperacionais newTaxasOperacionais = new TaxasOperacionais
        { TipoTaxa = ETaxasOperacionais.CDI.ToString("G"), ValorTaxa = 0.9, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now };
        var taxasOperacionais = this._mockTaxasOperacionaisRepository.GetAll().Result;
        Assert.AreEqual(2, taxasOperacionais.Count());
        _mockTaxasOperacionaisRepository.Add(newTaxasOperacionais);
        taxasOperacionais = this._mockTaxasOperacionaisRepository.GetAll().Result;
        Assert.AreEqual(3, taxasOperacionais.Count());
        TaxasOperacionais testTaxasOperacionais = this._mockTaxasOperacionaisRepository.GetById(3).Result;
        Assert.IsNotNull(testTaxasOperacionais);
        Assert.IsInstanceOfType(testTaxasOperacionais, typeof(TaxasOperacionais)); // Test type
        Assert.AreEqual(3, testTaxasOperacionais.ID);
    }
    [TestMethod]
    public void UpdateTaxasOperacionais()
    {
        TaxasOperacionais testTaxasOperacionais = this._mockTaxasOperacionaisRepository.GetById(2).Result;
        testTaxasOperacionais.ValorTaxa = 8;
        _mockTaxasOperacionaisRepository.Update(testTaxasOperacionais);
        var updatedImpostosOperacionais = _mockTaxasOperacionaisRepository.GetById(2).Result;
        Assert.AreEqual(8, updatedImpostosOperacionais.ValorTaxa);
    }
    [TestMethod]
    public void DeleteTaxasOperacionais()
    {
        TaxasOperacionais testTaxasOperacionais = this._mockTaxasOperacionaisRepository.GetById(1).Result;
        _mockTaxasOperacionaisRepository.Remove(testTaxasOperacionais);
        var deletedTaxasOperacionais = _mockTaxasOperacionaisRepository.GetById(1).Result;
        Assert.AreEqual(null, deletedTaxasOperacionais);
    }
}
