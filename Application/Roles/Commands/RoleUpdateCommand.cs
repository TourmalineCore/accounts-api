using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;
using Core.Entities;

namespace Application.Roles.Commands;

public class RoleUpdateCommand
{
    public long Id { get; set; }

    public string Name { get; set; }

    public List<string> Permissions { get; set; }

    public IEnumerable<Permission> GetRolePermissions()
    {
        return Permissions.Select(permissionName => new Permission(permissionName));
    }
}

public class RoleUpdateCommandHandler : ICommandHandler<RoleUpdateCommand>
{
    private readonly IRoleRepository _roleRepository;

    public RoleUpdateCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(RoleUpdateCommand command)
    {
        var dbRole = await _roleRepository.GetByIdAsync(command.Id);
        await ValidateRoleNameAsync(dbRole.Name, command.Name);
        dbRole.Update(command.Name, command.GetRolePermissions());
        await _roleRepository.UpdateAsync(dbRole);
    }

    private async Task ValidateRoleNameAsync(string currentName, string newName)
    {
        var roles = await _roleRepository.GetRolesAsync();
        var rolesNames = roles.Select(x => x.Name);

        if (currentName == newName)
        {
            return;
        }

        if (rolesNames.Contains(newName))
        {
            throw new ArgumentException($"Role with name [{newName}] already exists");
        }
    }
}