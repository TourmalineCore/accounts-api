using System.Threading.Tasks;
using UserManagementService.Application.Contracts;
using UserManagementService.Core.Contracts;
using UserManagementService.Core.Entities;

namespace UserManagementService.Application.Users.Commands
{
    public class CreateUserCommand
    {
        public string CorporateEmail { get; set; }
        public int RoleId { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<long> Handle(CreateUserCommand command)
        {
            var user = new User(
                command.CorporateEmail,
                command.RoleId
                );

            return await _userRepository.CreateAsync(user);
        }
    }
}
