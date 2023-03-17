using CalculoCDBRepository;
using CalculoCDBRepository.Base;
using CalculoCDBService;
using CalculoCDBService.Inferfaces;
using CalculoCDBService.Inferfaces.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculoCDBPresentation
{
    public class StartupApiTest
    {
        public StartupApiTest(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            ConfiguresServicesApplication(services);
            ConfiguresServicesRepository(services);
        }

        private void ConfiguresServicesApplication(IServiceCollection services)
        {
            services.AddScoped<IEfetuarCalculoService, EfetuarCalculoService>()
                    .AddScoped<IImpostoOperacionaisService, ImpostosOperacionaisService>()
                    .AddScoped<ITaxaOperacionaisService, TaxaOperacionaisService>()
                    .AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
        }

        private void ConfiguresServicesRepository(IServiceCollection services)
        {
            services.AddScoped<IImpostosOperacionaisRepository, ImpostosOperacionaisRepository>()
                    .AddScoped<ITaxasOperacionaisRepository, TaxasOperacionaisRepository>()
                    .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
