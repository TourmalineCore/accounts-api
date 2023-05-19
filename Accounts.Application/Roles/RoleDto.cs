using Accounts.Core.Entities;
using System.Collections.Generic;

namespace Accounts.Application.Roles;

public struct RoleDto
{
    public RoleDto(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        Permissions = role.Permissions;
    }

    public long Id { get; }

    public string Name { get; }

    public IEnumerable<string> Permissions { get; }
}
