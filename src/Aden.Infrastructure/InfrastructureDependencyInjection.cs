using Aden.Application.Interfaces;
using Aden.Infrastructure.Persistence;
using Aden.Infrastructure.Repositories;
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

        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
        services.AddScoped<ISpecificationRepository, SpecificationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>(); 
        
        return services;
    }
}