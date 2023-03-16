using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Commands
{
    public readonly struct UpdateUserCommand
    {
        public long Id { get; init; }
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string MiddleName { get; init; }

        public List<long> RoleIds { get; init; }
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public UpdateUserCommandHandler(IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        public async Task Handle(UpdateUserCommand request)
        {
            //TODO: #861ma1b6p - temporary disabled until we get prototypes
            var user = await _accountRepository.FindByIdAsync(request.Id);

            var newAccountRoles = (await _roleRepository.GetAllAsync())
                .Where(x => request.RoleIds.Contains(x.Id));

            user.Update(
                request.FirstName,
                request.LastName,
                request.MiddleName,
                newAccountRoles
            );

            await _accountRepository.UpdateAsync(user);
        }
    }
}
