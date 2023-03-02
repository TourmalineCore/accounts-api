using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Application.Privileges.Queries;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Privileges.Commands;
using Accounts.Application.Roles.Queries;
using Accounts.Application.HttpClients;
using Accounts.Application.Validators;
using Accounts.Application.Services;
using NodaTime;

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
            services.AddTransient<AddPrivilegeCommandHandler>();

            services.AddTransient<GetPrivilegeListQueryHandler>();
            services.AddTransient<GetPrivilegeByIdQueryHandler>();
            services.AddTransient<GetPrivilegesByAccountIdQueryHandler>();
            services.AddTransient<DeletePrivilegeCommandHandler>();

            services.AddScoped<AccountCreationCommandValidator>();
            services.AddTransient<IHttpClient, AccountHttpClient>();

            services.AddTransient<IClock, Clock>();

            return services;
        }
    }
}
