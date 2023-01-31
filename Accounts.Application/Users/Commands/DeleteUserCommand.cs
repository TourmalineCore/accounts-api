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
        private readonly IAccountRepository _accountRepository;
        private readonly IClock _clock;

        public DeleteUserCommandHandler(
            IAccountRepository accountRepository,
            IClock clock)
        {
            _accountRepository = accountRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteUserCommand request)
        {
            //TODO: #861ma1b6p - temporary disabled until we get prototypes
            // var user = await _accountRepository.FindByIdAsync(request.Id);
            //
            // user.Delete(_clock.GetCurrentInstant());
            //
            // await _accountRepository.UpdateAsync(user);
        }
    }

}
