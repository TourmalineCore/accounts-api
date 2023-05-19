using Microsoft.Extensions.DependencyInjection;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Roles.Queries;
using Accounts.Application.HttpClients;
using Accounts.Application.Validators;
using Accounts.Application.Services;
using NodaTime;
using Accounts.Application.Permissions.Queries;

namespace Accounts.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<AccountCreationCommandHandler>();
            services.AddTransient<UpdateUserCommandHandler>();
            services.AddTransient<DeleteUserCommandHandler>();
            services.AddTransient<AddRoleToUserCommandHandler>();

            services.AddTransient<GetAccountsQueryHandler>();
            services.AddTransient<GetAccountByIdQueryHandler>();

            services.AddTransient<GetRoleListQueryHandler>();
            services.AddTransient<GetRoleByIdQueryHandler>();
            services.AddTransient<DeleteRoleCommandHandler>();
            services.AddTransient<RoleCreationCommandHandler>();
            services.AddTransient<RoleUpdateCommandHandler>();

            services.AddTransient<GetPermissionsByAccountIdQueryHandler>();

            services.AddScoped<AccountCreationCommandValidator>();
            services.AddTransient<IHttpClient, AccountHttpClient>();

            services.AddTransient<IClock, Clock>();

            return services;
        }
    }
}
