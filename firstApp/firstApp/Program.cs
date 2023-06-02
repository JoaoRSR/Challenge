using App.Application;
using App.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace firstApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ServiceCollection? services = new();
        ConfigureServices(services);

        await services.AddSingleton<Executor, Executor>()
             .BuildServiceProvider()
             .GetService<Executor>()
             .Execute(args);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // build config
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        //services
        services.AddApplicationLayer();
        services.AddInfrastructureLayer(configuration);
    }
}