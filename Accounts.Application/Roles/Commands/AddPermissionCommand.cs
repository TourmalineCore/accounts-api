using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Application.Roles.Commands
{
    public class AddPermissionCommand
    {
        public long RoleId { get; set; }
        public List<long> PermissionId { get; set; }
    }

    public class AddPermissionCommandHandler : ICommandHandler<AddPermissionCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionsRepository _permissionsRepository;

        public AddPermissionCommandHandler(IRoleRepository roleRepository, IPermissionsRepository permissionsRepository)
        {
            _roleRepository = roleRepository;
            _permissionsRepository = permissionsRepository;
        }

        public async Task Handle(AddPermissionCommand request)
        {
            var role = await _roleRepository.FindByIdAsync(request.RoleId);
            var permissions = new List<Permission>();

            foreach (var x in request.PermissionId)
            {
                permissions.Add(await _permissionsRepository.FindByIdAsync(x));
            }

            await _roleRepository.UpdateRoleAsync(role, permissions);
        }
    }
}
