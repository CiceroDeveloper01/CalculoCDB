using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBDomain.Taxas;
using CalculoCDBRepository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CalculoCDBRepository.Taxas;

public class TaxasOperacionaisRepository : RepositoryBase<TaxasOperacionais>, ITaxasOperacionaisRepository
{
    public TaxasOperacionaisRepository(IConfiguration configuration,
                                       ILogger<TaxasOperacionaisRepository> logger) : base(configuration, logger)
    {

    }
}