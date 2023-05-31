using App.Application.Interfaces;
using App.Infrastructure.Dummyjson.Client;
using App.Infrastructure.Dummyjson.Configurations;
using App.Infrastructure.PostgresSQL.Configurations;
using App.Infrastructure.PostgresSQL.Models;
using App.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure;
public static class ProgramExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DummyjsonConfiguration>(configuration.GetSection(DummyjsonConfiguration.SectionName));
        services.Configure<PgConfiguration>(configuration.GetSection(PgConfiguration.SectionName));

        services.AddSingleton<IDummyjsonClient, DummyjsonClient>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IRepositoryService, RepositoryService>();

        PgConfiguration databaseConfiguration = configuration
            .GetSection(PgConfiguration.SectionName)
            .Get<PgConfiguration>();

        services.AddEntityFrameworkNpgsql().AddDbContext<ChallengeDbContext>(opt =>
        opt.UseNpgsql(databaseConfiguration.ConnectionString));

        return services;
    }
}