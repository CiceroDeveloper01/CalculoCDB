using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Enums;
using CalculoCDBDomain.OutPutDefault;
using CalculoCDBDomain.Taxas;
using CalculoCDBService.Inferfaces;
using CalculoCDBShared.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoCDBService
{
    public class EfetuarCalculoService :IEfetuarCalculoService
    {
        private readonly IImpostoOperacionaisService _impostoOperacionaisService;
        private readonly ITaxaOperacionaisService _taxaOperacionaisService;
        public EfetuarCalculoService(IImpostoOperacionaisService impostoOperacionaisService, ITaxaOperacionaisService taxaOperacionaisService)
        {
            _impostoOperacionaisService = impostoOperacionaisService;
            _taxaOperacionaisService = taxaOperacionaisService;
        }
        public async Task<ICommandResult> EfetuarCalculo(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
        {
            if (!valorInicialAplicaoDTO.ValidEntity())
                return await Task.FromResult(new CommandResult(false, "Por favor, corrija os campos abaixo", valorInicialAplicaoDTO.Notifications));
            else
                return await Task.FromResult(RealizarCalculo(valorInicialAplicaoDTO));
        }
        private ICommandResult RealizarCalculo(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
        {
            var taxasOperacionais = BuscarTaxasOperacionais();
            var impostosOperacionais = BuscaImpostoInvestimento(valorInicialAplicaoDTO);
            double valorFinal = 0.0;
            double valorInicial;
            double valorLiquido;
            valorInicial = valorInicialAplicaoDTO.ValorInicial;
            for(int i = 1; i <= valorInicialAplicaoDTO.PrazoInvestimento; i++)
            {
                valorFinal = valorInicial * (1 + ((taxasOperacionais.Item1.ValorTaxa/100) * (taxasOperacionais.Item2.ValorTaxa/100)));
                valorInicial = valorFinal;
            }
            valorLiquido = valorFinal - ((valorFinal / 100) * (impostosOperacionais/100));
            return new CommandResult(true, "O resultado dos Investimentos são", 
                                     new ResultadoInvestimentoDTO 
                                        { 
                                            PrazoInvestimento = valorInicialAplicaoDTO.PrazoInvestimento,
                                            ValorBruto = valorFinal,
                                            ValorLiquido = valorLiquido
                                        });  
        }
        private (TaxasOperacionais, TaxasOperacionais) BuscarTaxasOperacionais()
        {
            var taxas =  _taxaOperacionaisService.GetAll().Result;
            var taxaCDI = taxas.Where(x => x.TipoTaxa == ETaxasOperacionais.CDI.ToString("G")).FirstOrDefault();
            var taxaTB = taxas.Where(x => x.TipoTaxa == ETaxasOperacionais.TB.ToString("G")).FirstOrDefault();
            return (taxaCDI, taxaTB);
        }
        private double BuscaImpostoInvestimento(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
        {
            var impostosOperacionais = _impostoOperacionaisService.GetAll().Result;
            return impostosOperacionais.Where(x => valorInicialAplicaoDTO.PrazoInvestimento >= x.PrazoInicialCalculo  && valorInicialAplicaoDTO.PrazoInvestimento <= x.PrazoFinalCalculo).FirstOrDefault().ValorImposto;
        }
    }
}