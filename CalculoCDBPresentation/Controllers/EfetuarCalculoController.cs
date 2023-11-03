using CalculoCDBDomain.DTO;
using CalculoCDBService.Inferfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalculoCDB.Controllers;

[ApiController]
[Route("[controller]")]
public class EfetuarCalculoController : ControllerBase
{
    private readonly IEfetuarCalculoService _efetuarCalculoService;

    public EfetuarCalculoController(IEfetuarCalculoService efetuarCalculoService)
    {
        _efetuarCalculoService = efetuarCalculoService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ValorInicialAplicaoDTO valorInicialAplicaoDTO)
    {
        var result = await _efetuarCalculoService.EfetuarCalculo(valorInicialAplicaoDTO);
        if (result.Success)
            return Ok(result);
        else 
            return NotFound(result);
    }
}