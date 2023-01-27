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
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AddRoleToUserCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task Handle(AddRoleToUserCommand command)
        {
            var user = await _userRepository.FindByIdAsync(command.UserId);
            var role = await _roleRepository.FindByIdAsync(command.RoleId);

            await _userRepository.AddRoleAsync(user, role);
        }
    }
}
