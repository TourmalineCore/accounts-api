using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Commands
{
    public class UpdateUserCommand
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public long RoleId { get; set; }
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request)
        {

            var user = await _userRepository.FindByIdAsync(request.Id);

            user.Update(
                request.Email,
                request.RoleId
            );

            await _userRepository.UpdateAsync(user);
        }
    }
}
