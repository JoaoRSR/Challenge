using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application;
public static class ProgramExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // Register MediatR services
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}