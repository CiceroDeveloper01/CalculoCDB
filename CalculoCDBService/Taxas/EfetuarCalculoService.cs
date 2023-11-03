using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Enums;
using CalculoCDBDomain.Inferfaces;
using CalculoCDBDomain.OutPutDefault;
using CalculoCDBDomain.Taxas;
using CalculoCDBShared.Commands;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoCDBService.Taxas;

public class EfetuarCalculoService : IEfetuarCalculoService
{
    private readonly IImpostoOperacionaisService _impostoOperacionaisService;
    private readonly ITaxaOperacionaisService _taxaOperacionaisService;
    private readonly ILogger<EfetuarCalculoService> _logger;
    public EfetuarCalculoService(IImpostoOperacionaisService impostoOperacionaisService, 
                                 ITaxaOperacionaisService taxaOperacionaisService,
                                 ILogger<EfetuarCalculoService> logger)
    {
        _impostoOperacionaisService = impostoOperacionaisService;
        _taxaOperacionaisService = taxaOperacionaisService;
        _logger = logger;
    }
    public async Task<ICommandResult> EfetuarCalculo(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
    {
        _logger.LogInformation($@"Iniciando a Service: {typeof(EfetuarCalculoService)} e o Método: EfetuarCalculo");

        if (!valorInicialAplicaoDTO.ValidEntity())
            return await Task.FromResult(new CommandResult(false, "Por favor, corrija os campos abaixo", valorInicialAplicaoDTO.Notifications));
        else
            return await Task.FromResult(RealizarCalculo(valorInicialAplicaoDTO));
    }
    private ICommandResult RealizarCalculo(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
    {
        _logger.LogInformation($@"Executando a Service: {typeof(EfetuarCalculoService)} e o Método: RealizarCalculo");
        var taxasOperacionais = BuscarTaxasOperacionais();
        var impostosOperacionais = BuscaImpostoInvestimento(valorInicialAplicaoDTO);
        double valorFinal = 0.0;
        double valorInicial;
        double valorLiquido;
        valorInicial = valorInicialAplicaoDTO.ValorInicial;
        for (int i = 1; i <= valorInicialAplicaoDTO.PrazoInvestimento; i++)
        {
            valorFinal = valorInicial * (1 + taxasOperacionais.Item1.ValorTaxa / 100 * (taxasOperacionais.Item2.ValorTaxa / 100));
            valorInicial = valorFinal;
        }
        valorLiquido = valorFinal - valorFinal / 100 * (impostosOperacionais / 100);
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
        _logger.LogInformation($@"Executando a Service: {typeof(EfetuarCalculoService)} e o Método: BuscarTaxasOperacionais");
        var taxas = _taxaOperacionaisService.GetAll().Result;
        var taxaCDI = taxas.FirstOrDefault(x => x.TipoTaxa == ETaxasOperacionais.CDI.ToString("G"));
        var taxaTB = taxas.FirstOrDefault(x => x.TipoTaxa == ETaxasOperacionais.TB.ToString("G"));
        return (taxaCDI, taxaTB);
    }
    private double BuscaImpostoInvestimento(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
    {
        _logger.LogInformation($@"Executando a Service: {typeof(EfetuarCalculoService)} e o Método: BuscaImpostoInvestimento");
        var impostosOperacionais = _impostoOperacionaisService.GetAll().Result;
        var impostoOperacional = impostosOperacionais.FirstOrDefault(x => valorInicialAplicaoDTO.PrazoInvestimento >= x.PrazoInicialCalculo && valorInicialAplicaoDTO.PrazoInvestimento <= x.PrazoFinalCalculo);
        double valorImposto = 0;
        if (impostoOperacional != null)
            valorImposto = impostoOperacional.ValorImposto;
        return valorImposto;
    }
}