using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBRepository.Base;
using CalculoCDBRepository.Taxas;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace CalculoCDBRepository;

public static class DIRepository
{
    public static IServiceCollection AddDIRepository(this IServiceCollection services, string connection)
    {
        return services.AddScoped(IServiceProvider => new SqliteConnection(connection))
                       .AddScoped<IImpostosOperacionaisRepository, ImpostosOperacionaisRepository>()
                       .AddScoped<ITaxasOperacionaisRepository, TaxasOperacionaisRepository>()
                       .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}