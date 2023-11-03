using CalculoCDBDomain.DTO;
using CalculoCDBShared.Commands;
using System.Threading.Tasks;

namespace CalculoCDBDomain.Inferfaces;

public interface IEfetuarCalculoService
{
    Task<ICommandResult> EfetuarCalculo(ValorInicialAplicaoDto valorInicialAplicaoDTO);
}