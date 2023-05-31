using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;
using Core.Entities;
using Core.Models;

namespace Application.Roles.Commands;

public readonly struct RoleCreationCommand
{
    public string Name { get; init; }

    public List<string> Permissions { get; init; }

    public IEnumerable<Permission> GetRolePermissions()
    {
        return Permissions.Select(permissionName => new Permission(permissionName));
    }
}

public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
{
    private readonly IRolesRepository _rolesRepository;

    public RoleCreationCommandHandler(IRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task HandleAsync(RoleCreationCommand command)
    {
        await ValidateRoleNameAsync(command.Name);
        await _rolesRepository.CreateAsync(new Role(command.Name, command.GetRolePermissions()));
    }

    private async Task ValidateRoleNameAsync(string name)
    {
        var roles = await _rolesRepository.GetAllAsync();
        var roleNames = roles.Select(x => x.Name);

        if (roleNames.Contains(name))
        {
            throw new ArgumentException($"Role with name [{name}] already exists");
        }
    }
}