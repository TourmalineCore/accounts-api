using System.Collections.Generic;

namespace UserManagementService.Core.Entities;

public enum PrivilegesNames
{
    CanManageEmployees = 1,
    CanViewAnalytic,
    CanViewFinanceForPayroll
}

public class Privilege : IIdentityEntity
{
    public long Id { get; private set; }

    public PrivilegesNames Name { get; private set; }

    public List<Role> Roles { get; private set; }

    // To Db Context
    private Privilege()
    {
    }
    public Privilege(long id, PrivilegesNames name)
    {
        Id = id;
        Name = name;
    }
}
