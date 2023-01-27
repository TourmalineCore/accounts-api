using Accounts.Application.Contracts;
using Accounts.Application.HttpClients;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Commands
{
    public class CreateUserCommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CorporateEmail { get; set; }

        public List<long> RoleIds { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IHttpClient _httpClient;

        public CreateUserCommandHandler(
            IUserRepository userRepository, 
            IValidator<CreateUserCommand> validator, 
            IHttpClient httpClient,
            IRoleRepository roleRepository
            )
        {
            _userRepository = userRepository;
            _validator = validator;
            _httpClient = httpClient;
            _roleRepository = roleRepository;
        }

        public async Task<long> Handle(CreateUserCommand command)
        {
            var resultValidation = await _validator.ValidateAsync(command);

            if (!resultValidation.IsValid)
            {
                return -1;
            }

            var roles = await _roleRepository.FindListAsync(command.RoleIds);

            var user = new Account(command.CorporateEmail, roles);

            var id = await _userRepository.CreateAsync(user);

            await _httpClient.SendDataAuthApi(id, user.Email);

            return id;
        }
    }
}
