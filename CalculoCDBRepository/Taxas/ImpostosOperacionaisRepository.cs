using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBDomain.Taxas;
using CalculoCDBRepository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Dapper.SqlMapper;

namespace CalculoCDBRepository.Taxas;

public class ImpostosOperacionaisRepository : RepositoryBase<ImpostosOperacionais>, IImpostosOperacionaisRepository
{
    public ImpostosOperacionaisRepository(IConfiguration configuration,
                                          ILogger<ImpostosOperacionaisRepository> logger) : base(configuration, logger)
    {
    }
}