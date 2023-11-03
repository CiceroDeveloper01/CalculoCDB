using CalculoCDBService.Inferfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CalculoCDBService;

public static class DIService
{
    public static IServiceCollection AddDIService(this IServiceCollection services)
    {
        return services.AddScoped<IEfetuarCalculoService, EfetuarCalculoService>()
                       .AddScoped<IImpostoOperacionaisService, ImpostosOperacionaisService>()
                       .AddScoped<ITaxaOperacionaisService, TaxaOperacionaisService>()
                       .AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
    }
}
