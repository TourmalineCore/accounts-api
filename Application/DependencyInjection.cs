using Application.HttpClients;
using Application.Roles.Commands;
using Application.Roles.Queries;
using Application.Services;
using Application.Users.Commands;
using Application.Users.Queries;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<AccountCreationCommandHandler>();
            services.AddTransient<AccountUpdateCommandHandler>();

            services.AddTransient<GetAccountsQueryHandler>();
            services.AddTransient<GetAccountByIdQueryHandler>();

            services.AddTransient<GetRoleListQueryHandler>();
            services.AddTransient<GetRoleByIdQueryHandler>();
            services.AddTransient<DeleteRoleCommandHandler>();
            services.AddTransient<RoleCreationCommandHandler>();
            services.AddTransient<RoleUpdateCommandHandler>();

            services.AddTransient<GetPermissionsByAccountIdQueryHandler>();

            services.AddScoped<AccountCreationCommandValidator>();
            services.AddScoped<AccountUpdateCommandValidator>();
            services.AddTransient<IHttpClient, AccountHttpClient>();

            services.AddTransient<AccountBlockCommand>();
            services.AddTransient<AccountUnblockCommand>();

            services.AddTransient<IClock, Clock>();

            return services;
        }
    }
}
