using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;
using Core.Models;

namespace Application.Roles.Commands;

public readonly struct RoleUpdateCommand
{
  public long Id { get; init; }

  public string Name { get; init; }

  public List<string> Permissions { get; init; }

  public IEnumerable<Permission> GetRolePermissions()
  {
    return Permissions.Select(permissionName => new Permission(permissionName));
  }
}

public class RoleUpdateCommandHandler : ICommandHandler<RoleUpdateCommand>
{
  private readonly IRolesRepository _rolesRepository;

  public RoleUpdateCommandHandler(IRolesRepository rolesRepository)
  {
    _rolesRepository = rolesRepository;
  }

  public async Task HandleAsync(RoleUpdateCommand command)
  {
    var role = await _rolesRepository.GetByIdAsync(command.Id);
    await ValidateNewRoleNameAsync(role.Name, command.Name);
    role.Update(command.Name, command.GetRolePermissions());
    await _rolesRepository.UpdateAsync(role);
  }

  private async Task ValidateNewRoleNameAsync(string currentName, string newName)
  {
    var roles = await _rolesRepository.GetAllAsync();
    var roleNames = roles.Select(x => x.Name);

    if (currentName == newName)
    {
      return;
    }

    if (roleNames.Contains(newName))
    {
      throw new ArgumentException($"Role with name [{newName}] already exists");
    }
  }
}
