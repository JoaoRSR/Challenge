using App.Application.Interfaces;
using App.Infrastructure.Dummyjson.Client;
using App.Infrastructure.Dummyjson.Configurations;
using App.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure;
public static class ProgramExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DummyjsonConfiguration>(configuration.GetSection(DummyjsonConfiguration.SectionName));
        services.AddSingleton<IDummyjsonClient, DummyjsonClient>();
        services.AddSingleton<IDataService, DataService>();

        return services;
    }
}