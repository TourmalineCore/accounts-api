using FluentValidation;
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
        private IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<long> Handle(CreateUserCommand command)
        {
            var resultValidation = await _validator.ValidateAsync(command);

            if (!resultValidation.IsValid)
            {
                return -1;
            }

            var user = new User(
                command.CorporateEmail,
                command.RoleId
                );

            return await _userRepository.CreateAsync(user);
        }
    }
}
