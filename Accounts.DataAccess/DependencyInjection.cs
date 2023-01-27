using Accounts.Core.Contracts;
using Accounts.DataAccess.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Accounts.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AccountsDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                                o => o.UseNodaTime());
            });

            services.AddScoped<AccountsDbContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IPrivilegeRepository, PrivilegeRepository>();

            return services;
        }
    }
}
