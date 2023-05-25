using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities;

public class Role : IIdentityEntity
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public List<AccountRole> AccountRoles { get; private set; }

    public string[] Permissions { get; private set; } = Array.Empty<string>();

    public Role(string name)
    {
        Name = name;
    }

    public Role(long id, string name, IEnumerable<Permission> permissions)
    {
        Id = id;
        Name = name;
        SetPermissions(permissions);
    }

    public Role(string name, IEnumerable<Permission> permissions)
    {
        Name = name;
        SetPermissions(permissions);
    }

    public void Update(string name, IEnumerable<Permission> permissions)
    {
        Name = name;
        SetPermissions(permissions);
    }

    public void UpdateRole(IEnumerable<Permission> permissions)
    {
        SetPermissions(permissions);
    }

    private void SetPermissions(IEnumerable<Permission> permissions)
    {
        Permissions = permissions.Select(x => x.Name).ToArray();
    }

    private Role()
    {
    }
}