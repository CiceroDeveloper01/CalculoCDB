using CalculoCDBShared;
using FluentValidator;

namespace CalculoCDBDomain.DTO;

public class ValorInicialAplicaoDTO : Notifiable, ICommand
{
    public double ValorInicial { get; set; }
    public int PrazoInvestimento { get; set; }

    public bool ValidEntity()
    {
        if (ValorInicial <= 0)
            AddNotification("ValorInicial", "Valor Inicial de Investimento Mínímo de R$ 1,00, Por Favor, Informar um valor Maior.");

        if (PrazoInvestimento < 2)
            AddNotification("PrazoInvestimento", "O Prazo Mínimo Para Investimento é de 2 Dois Meses, Por Favor, Informar um Valor Maior.");


        return Valid;
    }
}