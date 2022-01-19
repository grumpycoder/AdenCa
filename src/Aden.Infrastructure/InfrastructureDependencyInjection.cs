using Aden.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aden.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DomainEventDispatcher>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .AddInterceptors(services.BuildServiceProvider().GetService<DomainEventDispatcher>())
                .UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            )
        );


        return services;
    }
}