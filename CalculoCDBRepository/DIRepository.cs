using CalculoCDBRepository.Base;
using CalculoCDBService.Inferfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CalculoCDBRepository;

public static class DIRepository
{
    public static IServiceCollection AddDIRepository(this IServiceCollection services, string connection)
    {
        return services.AddScoped<IImpostosOperacionaisRepository, ImpostosOperacionaisRepository>()
                       .AddScoped<ITaxasOperacionaisRepository, TaxasOperacionaisRepository>()
                       .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}