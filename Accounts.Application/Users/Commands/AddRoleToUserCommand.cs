using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Commands
{
    public class AddRoleToUserCommand
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }
    }

    public class AddRoleToUserCommandHandler : ICommandHandler<AddRoleToUserCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public AddRoleToUserCommandHandler(
            IAccountRepository accountRepository,
            IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        public async Task Handle(AddRoleToUserCommand command)
        {
            //TODO: #861ma1b6p - temporary disabled until we get prototypes
            // var user = await _accountRepository.FindByIdAsync(command.UserId);
            // var role = await _roleRepository.FindByIdAsync(command.RoleId);
            //
            // await _accountRepository.AddRoleAsync(user, role);
        }
    }
}
