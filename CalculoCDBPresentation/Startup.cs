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
using Microsoft.OpenApi.Models;


namespace CalculoCDB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CalculoCDB", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                builder =>
                {
                    builder.WithOrigins(
                                        "http://localhost:4200"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalculoCDB v1"));
            }

            app.UseCors("AllowAngularOrigins");

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
