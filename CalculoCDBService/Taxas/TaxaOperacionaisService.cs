using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Inferfaces;
using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBDomain.Taxas;
using CalculoCDBService.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CalculoCDBService.Taxas;

public class TaxaOperacionaisService : ServiceBase<TaxasOperacionais>, ITaxaOperacionaisService
{
    private readonly IRepositoryBase<TaxasOperacionais> _repositoryBase;
    private readonly ILogger<TaxaOperacionaisService> _logger;
    public TaxaOperacionaisService(IRepositoryBase<TaxasOperacionais> repositoryBase,
                                   ILogger<TaxaOperacionaisService> logger) : base(repositoryBase)
    {
        _logger = logger;
        _repositoryBase = repositoryBase;
        CreateTabletemporary();
        PopulateImpostosOperacionais();
    }

    /// <summary>
    /// Método criado para simular um banco de dados onde buscamos os Impostos Operacionais 
    /// </summary>
    private void CreateTabletemporary()
    {
        _logger.LogInformation($@"Executando a Service: {typeof(TaxaOperacionaisService)} e o Método: CreateTabletemporary");
        var commandCreate = @$"CREATE TABLE 
                                   IF NOT EXISTS tb_TaxasOperacionais(Id int
                                                                    , TipoTaxa VarChar(100)
                                                                    , ValorTaxa double
                                                                    , DataAlteracao DateTime
                                                                    , DataCriacao DateTime)";
        _repositoryBase.CreateTable(commandCreate);
    }


    /// <summary>
    /// Método criado temporário para armazenar os Impostos para Cálculo
    /// </summary>
    private void PopulateImpostosOperacionais()
    {
        _logger.LogInformation($@"Executando a Service: {typeof(TaxaOperacionaisService)} e o Método: PopulateImpostosOperacionais");
        List<TaxasOperacionais> taxasOperacionais = new List<TaxasOperacionais>();
        taxasOperacionais.Add(new TaxasOperacionais { ID = 1, TipoTaxa = ETaxasOperacionais.TB.ToString("G"), ValorTaxa = 108.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        taxasOperacionais.Add(new TaxasOperacionais { ID = 2, TipoTaxa = ETaxasOperacionais.CDI.ToString("G"), ValorTaxa = 0.9, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
        foreach (var taxaOperacional in taxasOperacionais)
        {
            var taxaOperacionalLocalizada = _repositoryBase.GetById(taxaOperacional.ID).Result;
            if (taxaOperacionalLocalizada == null)
                _repositoryBase.Add(taxaOperacional);
        }
    }
}