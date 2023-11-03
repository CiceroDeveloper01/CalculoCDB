using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Taxas;
using CalculoCDBService.Inferfaces.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoCDBTest;

public static class DataFixtures
{
    private static IList<ImpostosOperacionais> ListImpostosOperacinais()
    {
        IList<ImpostosOperacionais> impostosOperacionais = new List<ImpostosOperacionais>();
        impostosOperacionais.Add(new ImpostosOperacionais { ID = 1, TempoCalculado = EImpostosTempoInvestimento.AteSeisMeses.ToString("G"), PrazoInicialCalculo = 1, PrazoFinalCalculo = 6, ValorImposto = 22.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now }); ;
        impostosOperacionais.Add(new ImpostosOperacionais { ID = 2, TempoCalculado = EImpostosTempoInvestimento.AteDozeMeses.ToString("G"), PrazoInicialCalculo = 7, PrazoFinalCalculo = 12, ValorImposto = 22.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        impostosOperacionais.Add(new ImpostosOperacionais { ID = 3, TempoCalculado = EImpostosTempoInvestimento.AteVinteQuatroMeses.ToString("G"), PrazoInicialCalculo = 13, PrazoFinalCalculo = 24, ValorImposto = 17.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        impostosOperacionais.Add(new ImpostosOperacionais { ID = 4, TempoCalculado = EImpostosTempoInvestimento.Acima24Meses.ToString("G"), PrazoInicialCalculo = 25, PrazoFinalCalculo = 1000, ValorImposto = 15.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        return impostosOperacionais;
    }

    private static IList<TaxasOperacionais> ListTaxasOperacionais()
    {
        IList<TaxasOperacionais> taxasOperacionais = new List<TaxasOperacionais>();
        taxasOperacionais.Add(new TaxasOperacionais { ID = 1, TipoTaxa = ETaxasOperacionais.TB.ToString("G"), ValorTaxa = 108.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        taxasOperacionais.Add(new TaxasOperacionais { ID = 2, TipoTaxa = ETaxasOperacionais.CDI.ToString("G"), ValorTaxa = 0.9, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        return taxasOperacionais;
    }

    public static Mock<IRepositoryBase<TaxasOperacionais>> MockTaxasOperacionaisRepository()
    {
        var taxasOperacionais = ListTaxasOperacionais();

        Mock<IRepositoryBase<TaxasOperacionais>> mockTaxasOperacionaisRepository = new Mock<IRepositoryBase<TaxasOperacionais>>();

        mockTaxasOperacionaisRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<TaxasOperacionais>)taxasOperacionais));

        mockTaxasOperacionaisRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) =>
        {
            var taxaLocalizada = taxasOperacionais.Where(x => x.ID == i).FirstOrDefault();
            return Task.FromResult(taxaLocalizada);
        });


        mockTaxasOperacionaisRepository.Setup(mr => mr.Add(It.IsAny<TaxasOperacionais>())).Returns(
            (TaxasOperacionais target) =>
            {
                DateTime now = DateTime.Now;
                if (target.ID.Equals(default(int)))
                {
                    target.DataCriacao = now;
                    target.DataAlteracao = now;
                    target.ID = taxasOperacionais.Count() + 1;
                    taxasOperacionais.Add(target);
                }
                else
                {
                    var original = taxasOperacionais.Where(q => q.ID == target.ID).Single();
                    if (original == null)
                        return Task.FromResult(false);
                    original.TipoTaxa = target.TipoTaxa;
                    original.ValorTaxa = target.ValorTaxa;
                    original.DataAlteracao = now;
                }
                return Task.FromResult(true);
            });

        mockTaxasOperacionaisRepository.Setup(mr => mr.Remove(It.IsAny<TaxasOperacionais>())).Returns(
                    (TaxasOperacionais target) =>
                    {
                        taxasOperacionais.Remove(target);
                        return Task.FromResult(true);
                    });

        return mockTaxasOperacionaisRepository;
    }

    public static Mock<IRepositoryBase<ImpostosOperacionais>> MockImpostosOperacionaisRepository()
    {
        var impostosOperacionais = ListImpostosOperacinais();

        Mock<IRepositoryBase<ImpostosOperacionais>> mockImpostosOperacionaisRepository = new Mock<IRepositoryBase<ImpostosOperacionais>>();

        mockImpostosOperacionaisRepository.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<ImpostosOperacionais>)impostosOperacionais));

        mockImpostosOperacionaisRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) =>
        {
            var impostoLocalizado = impostosOperacionais.Where(x => x.ID == i).FirstOrDefault();
            return Task.FromResult(impostoLocalizado);
        });


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

        mockImpostosOperacionaisRepository.Setup(mr => mr.Remove(It.IsAny<ImpostosOperacionais>())).Returns(
                    (ImpostosOperacionais target) =>
                    {
                        impostosOperacionais.Remove(target);
                        return Task.FromResult(true);
                    });

        return mockImpostosOperacionaisRepository;
    }
}