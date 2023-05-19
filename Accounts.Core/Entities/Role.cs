using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Core.Entities;

public enum RoleNames
{
    Admin = 1,
    CEO,
    Manager,
    Employee,
}

public class Role : IIdentityEntity
{
    public long Id { get; private set; }

    public RoleNames Name { get; private set; }

    public List<AccountRole> AccountRoles { get; private set; }

    public string[] Permissions { get; private set; } = Array.Empty<string>();

    public string NormalizedName => Name.ToString().Normalize();

    public Role(RoleNames name)
    {
        Name = name;
    }

    public Role(long id, RoleNames name, IEnumerable<Permission> permissions)
    {
        Id = id;
        Name = name;
        SetPermissions(permissions);
    }

    public Role(RoleNames name, IEnumerable<Permission> permissions)
    {
        Name = name;
        SetPermissions(permissions);
    }

    public void Update(RoleNames name)
    {
        Name = name;
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