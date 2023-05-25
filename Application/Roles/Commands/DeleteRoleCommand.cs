using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Roles.Commands
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
            var permission = await _roleRepository.FindByIdAsync(request.Id);

            await _roleRepository.RemoveAsync(permission);
        }
    }
}
