using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Privileges.Commands
{
    public class DeletePrivilegeCommand
    {
        public long Id { get; set; }
    }

    public class DeletePrivilegeCommandHandler : ICommandHandler<DeletePrivilegeCommand>
    {
        private readonly IPrivilegeRepository _privilegeRepository;

        public DeletePrivilegeCommandHandler(IPrivilegeRepository privilegeRepository)
        {
            _privilegeRepository = privilegeRepository;
        }

        public async Task Handle(DeletePrivilegeCommand request)
        {
            var privilege = await _privilegeRepository.FindByIdAsync(request.Id);

            await _privilegeRepository.RemoveAsync(privilege);
        }
    }
}
