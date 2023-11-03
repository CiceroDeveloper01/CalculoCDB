using CalculoCDBDomain.DTO;
using CalculoCDBShared.Commands;
using System.Threading.Tasks;

namespace CalculoCDBService.Inferfaces;

public interface IEfetuarCalculoService
{
    Task<ICommandResult> EfetuarCalculo(ValorInicialAplicaoDTO valorInicialAplicaoDTO);
}