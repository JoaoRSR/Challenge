using App.Application;
using App.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace firstApp;

internal class Program
{
    private static void Main(string[] args)
    {
        ServiceCollection? services = new();
        ConfigureServices(services);
        services.AddSingleton<Executor, Executor>()
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
        //.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())

        services.AddApplicationLayer();
        services.AddInfrastructureLayer(configuration);
    }

    public class Executor
    {
        public Executor()
        {
        }

        public async void Execute(string[] args)
        {
            Console.WriteLine("todo");
        }
    }
}