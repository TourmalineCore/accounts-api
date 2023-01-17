using FluentValidation;
using System.Threading.Tasks;
using UserManagementService.Application.Contracts;
using UserManagementService.Application.HttpClients;
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
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IHttpClient _httpClient;

        public CreateUserCommandHandler(IUserRepository userRepository, IValidator<CreateUserCommand> validator, IHttpClient httpClient)
        {
            _userRepository = userRepository;
            _validator = validator;
            _httpClient = httpClient;
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

            var id = await _userRepository.CreateAsync(user);

            await _httpClient.SendDataAuthApi(id, user.Email);

            return id;
        }
    }
}
