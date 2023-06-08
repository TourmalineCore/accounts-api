using Core.Entities;
using Core.Models;

namespace Tests.TestsData;

public static class TestData
{
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Ceo = "Ceo";
        public const string Manager = "Manager";
        public const string Employee = "Employee";
    }

    public static readonly List<Role> ValidAccountRoles = new()
    {
        new Role(BaseRoleNames.Ceo),
    };

    public static readonly List<Role> AllRoles = new()
    {
        new Role(1,
                RoleNames.Admin,
                new List<Permission>
                {
                    new(Permissions.ViewAccounts),
                    new(Permissions.ViewRoles),
                    new(Permissions.ManageRoles),
                }
            ),
        new Role(2,
                RoleNames.Ceo,
                new List<Permission>
                {
                    new(Permissions.ViewAccounts),
                    new(Permissions.ViewRoles),
                }
            ),
        new Role(3,
                RoleNames.Manager,
                new List<Permission>
                {
                    new(Permissions.ViewAccounts),
                }
            ),
        new Role(4, RoleNames.Employee, new List<Permission>()),
    };
}