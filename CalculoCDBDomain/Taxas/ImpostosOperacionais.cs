using CalculoCDBShared.Attribute;

namespace CalculoCDBDomain.Taxas;

[TableDataBaseNameAttribute(Name = "tb_ImpostosOperacionais", Description = "Tabela que Armazena os Dados dos Impostos a Serem Recolhidos pós Líquidação de Investimentos")]
public class ImpostosOperacionais : AbstractEntity
{
    public string TempoCalculado { get; set; }
    public int PrazoInicialCalculo { get; set; }
    public int PrazoFinalCalculo { get; set; }
    public double ValorImposto { get; set; }
}