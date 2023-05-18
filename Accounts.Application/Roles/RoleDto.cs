using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Application.Roles;

public struct RoleDto
{
    public RoleDto(Role role)
    {
        Id = role.Id;
        Name = role.NormalizedName;
        Permissions = role.Privileges.Select(x => x.Name).ToList();
    }

    public long Id { get; }

    public string Name { get; }

    public List<string> Permissions { get; }
}
