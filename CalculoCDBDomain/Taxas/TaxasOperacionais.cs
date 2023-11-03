using CalculoCDBShared.Attribute;

namespace CalculoCDBDomain.Taxas;

[TableDataBaseNameAttribute(Name = "tb_TaxasOperacionais", Description = "Tabela que Armazena as Taxas Operacionais para Cálculo de Rendimento")]
public class TaxasOperacionais : AbstractEntity
{
    public string TipoTaxa { get; set; }
    public double ValorTaxa { get; set; }
}