using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Roles.Commands
{
    public class DeleteRoleCommand
    {
        public long Id { get; set; }
    }

    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(DeleteRoleCommand request)
        {
            var privilege = await _roleRepository.FindByIdAsync(request.Id);

            await _roleRepository.RemoveAsync(privilege);
        }
    }
}
