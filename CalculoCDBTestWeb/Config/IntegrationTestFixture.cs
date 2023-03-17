using Microsoft.Extensions.DependencyInjection;


namespace CalculoCDBTestWeb.Config
{
    public class IntegrationTestFixture
    {
        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }

    }
}
