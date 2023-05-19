using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;

namespace Accounts.Application.Roles.Commands;

public class RoleCreationCommand
{
    public string Name { get; set; }

    public List<string> Permissions { get; set; }

    public IEnumerable<Permission> GetRolePermissions()
    {
        return Permissions.Select(permissionName => new Permission(permissionName));
    }
}

public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
{
    private readonly IRoleRepository _roleRepository;

    public RoleCreationCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(RoleCreationCommand command)
    {
        await ValidateRoleName(command.Name);
        await _roleRepository.CreateAsync(new Role(command.Name, command.GetRolePermissions()));
    }

    private async Task ValidateRoleName(string name)
    {
        var roles = await _roleRepository.GetRolesAsync();
        var rolesNames = roles.Select(x => x.Name);

        if (rolesNames.Contains(name))
        {
            throw new ArgumentException($"Role with name [{name}] already exists");
        }
    }
}