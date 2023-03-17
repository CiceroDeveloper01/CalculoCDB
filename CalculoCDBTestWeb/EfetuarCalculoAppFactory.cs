using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CalculoCDBTestWeb
{
    public class EfetuarCalculoAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Development");
        }
    }
}
