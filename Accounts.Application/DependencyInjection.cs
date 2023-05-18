using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Roles.Queries;
using Accounts.Application.HttpClients;
using Accounts.Application.Validators;
using Accounts.Application.Services;
using NodaTime;
using Accounts.Application.Permissions.Queries;
using Accounts.Application.Permissions.Commands;

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
            services.AddTransient<AddPermissionCommandHandler>();

            services.AddTransient<GetPermissionsQueryHandler>();
            services.AddTransient<GetPermissionByIdQueryHandler>();
            services.AddTransient<GetPermissionsByAccountIdQueryHandler>();
            services.AddTransient<DeletePermissionCommandHandler>();

            services.AddScoped<AccountCreationCommandValidator>();
            services.AddTransient<IHttpClient, AccountHttpClient>();

            services.AddTransient<IClock, Clock>();

            return services;
        }
    }
}
