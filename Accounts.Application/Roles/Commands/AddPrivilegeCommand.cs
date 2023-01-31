using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Application.Roles.Commands
{
    public class AddPrivilegeCommand
    {
        public long RoleId { get; set; }
        public List<long> PrivilegeId { get; set; }
    }

    public class AddPrivilegeCommandHandler : ICommandHandler<AddPrivilegeCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPrivilegeRepository _privilegeRepository;

        public AddPrivilegeCommandHandler(IRoleRepository roleRepository, IPrivilegeRepository privilegeRepository)
        {
            _roleRepository = roleRepository;
            _privilegeRepository = privilegeRepository;
        }

        public async Task Handle(AddPrivilegeCommand request)
        {
            var role = await _roleRepository.FindByIdAsync(request.RoleId);
            var privileges = new List<Privilege>();

            foreach (var x in request.PrivilegeId)
            {
                privileges.Add(await _privilegeRepository.FindByIdAsync(x));
            }

            await _roleRepository.UpdateRoleAsync(role, privileges);
        }
    }
}
