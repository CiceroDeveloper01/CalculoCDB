using CalculoCDBDomain.DTO;
using CalculoCDBDomain.Inferfaces;
using CalculoCDBDomain.OutPutDefault;
using CalculoCDBService.Taxas;
using CalculoCDBShared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CalculoCDB.Controllers;

[ApiController]
[Route("v1/")]
public class EfetuarCalculoController : ControllerBase
{
    private readonly IEfetuarCalculoService _efetuarCalculoService;
    private readonly ILogger<EfetuarCalculoController> _logger;

    public EfetuarCalculoController(IEfetuarCalculoService efetuarCalculoService,
                                    ILogger<EfetuarCalculoController> logger)
    {
        _efetuarCalculoService = efetuarCalculoService;
        _logger = logger;
    }

    [HttpPost]
    [Route("Calcular")]
    public async Task<IActionResult> Post(ValorInicialAplicaoDto valorInicialAplicaoDTO)
    {
        try
        {
            var result = await _efetuarCalculoService.EfetuarCalculo(valorInicialAplicaoDTO);
            if (result.Success)
                return StatusCode((int)ERetornosApi.Ok, result);
            else
                return StatusCode((int)ERetornosApi.NotFound, result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($@"Erro a Controller: {typeof(EfetuarCalculoController)} e o Método: Post a Mensagem: {ex.Message}");
            var error = new CommandResult(false, "Houve um erro interno ao tentar efetuar o cálculo, por favor, tente mais tarde", null);
            return StatusCode((int)ERetornosApi.InternalServerError, error);
        }
    }
}