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
        private readonly IAccountRepository _accountRepository;

        public UpdateUserCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(UpdateUserCommand request)
        {
            //TODO: #861ma1b6p - temporary disabled until we get prototypes
            // var user = await _accountRepository.FindByIdAsync(request.Id);
            //
            // user.Update(
            //     request.Email,
            //     request.RoleId
            // );
            //
            // await _accountRepository.UpdateAsync(user);
        }
    }
}
