using System.Threading.Tasks;
using NodaTime;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;

namespace Accounts.Application.Users.Commands
{
    public class DeleteUserCommand
    {
        public long Id { get; set; }
    }

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IClock _clock;

        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IClock clock)
        {
            _userRepository = userRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteUserCommand request)
        {
            var user = await _userRepository.FindByIdAsync(request.Id);

            user.Delete(_clock.GetCurrentInstant());

            await _userRepository.UpdateAsync(user);
        }
    }

}
