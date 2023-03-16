using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Taxas;
using CalculoCDBService.Inferfaces;
using CalculoCDBService.Inferfaces.Repository;
using System.Collections.Generic;

namespace CalculoCDBService
{
    public class ImpostosOperacionaisService : ServiceBase<ImpostosOperacionais>, IImpostoOperacionaisService
    {
        private readonly IRepositoryBase<ImpostosOperacionais> _repositoryBase;
        public ImpostosOperacionaisService(IRepositoryBase<ImpostosOperacionais> repositoryBase) : base(repositoryBase)
        {
            _repositoryBase = repositoryBase;
            CreateTabletemporary();
            PopulateImpostosOperacionais();
        }

        /// <summary>
        /// Método criado para simular um banco de dados onde buscamos os Impostos Operacionais 
        /// </summary>
        private void CreateTabletemporary()
        {
            var commandCreate = "CREATE TABLE IF NOT EXISTS TB_ImpostosOperacionais(Id int, TempoCalculado VARCHAR(100), PrazoInicialCalculo int, PrazoFinalCalculo int, ValorImposto double, DataAlteracao DateTime, DataCriacao DateTime)";
            _repositoryBase.CreateTable(commandCreate);
        }

        /// <summary>
        /// Método criado temporário para armazenar os Impostos para Cálculo
        /// </summary>
        private void PopulateImpostosOperacionais()
        {
            List<ImpostosOperacionais> impostosOperacionais = new List<ImpostosOperacionais>();
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 1, TempoCalculado = EImpostosTempoInvestimento.AteSeisMeses.ToString("G"), PrazoInicialCalculo = 1, PrazoFinalCalculo = 6,  ValorImposto = 22.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now }); ;
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 2, TempoCalculado = EImpostosTempoInvestimento.AteDozeMeses.ToString("G"), PrazoInicialCalculo = 7, PrazoFinalCalculo = 12, ValorImposto = 22.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 3, TempoCalculado = EImpostosTempoInvestimento.AteVinteQuatroMeses.ToString("G"), PrazoInicialCalculo = 13, PrazoFinalCalculo = 24, ValorImposto = 17.50, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
            impostosOperacionais.Add(new ImpostosOperacionais { ID = 4, TempoCalculado = EImpostosTempoInvestimento.Acima24Meses.ToString("G"), PrazoInicialCalculo = 25, PrazoFinalCalculo = 1000, ValorImposto = 15.00, DataAlteracao = System.DateTime.Now, DataCriacao = System.DateTime.Now });
            foreach(var impostoOperacional in impostosOperacionais)
            {
                var impostoOperacionalCapturado = _repositoryBase.GetById(impostoOperacional.ID).Result;
                if (impostoOperacionalCapturado == null)
                    _repositoryBase.Add(impostoOperacional);
            }
        }
    }
}