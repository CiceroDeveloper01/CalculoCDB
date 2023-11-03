using System;

namespace CalculoCDBDomain;

public class AbstractEntity
{
    public int ID { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
}