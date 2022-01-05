using Aden.WebUI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Aden.WebUI;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));


        return services;
    }
}