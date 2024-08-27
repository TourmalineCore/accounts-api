using Core.Contracts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnection);

        services.AddDbContext<AccountsDbContext>(options =>
                {
                    options.UseNpgsql(connectionString,
                            o => o.UseNodaTime()
                        );
                }
            );

        services.AddScoped<AccountsDbContext>();

        services.AddTransient<IAccountsRepository, AccountsRepository>();
        services.AddTransient<IRolesRepository, RolesRepository>();
        services.AddTransient<ITenantsRepository, TenantsRepository>();

        return services;
    }
}