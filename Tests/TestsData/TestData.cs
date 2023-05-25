using Core.Entities;

namespace Tests.TestsData
{
    public static class TestData
    {
        public static class RoleNames
        {
            public const string Admin = "Admin";
            public const string Ceo = "Ceo";
            public const string Manager = "Manager";
            public const string Employee = "Employee";
        }

        public static readonly List<Role> Roles = new()
        {
            new Role(1,
                    RoleNames.Admin,
                    new List<Permission>
                    {
                        new(Permissions.EditFullEmployeesData),
                    }
                ),
            new Role(2,
                    RoleNames.Ceo,
                    new List<Permission>
                    {
                        new(Permissions.EditFullEmployeesData),
                        new(Permissions.AccessAnalyticalForecastsPage),
                    }
                ),
            new Role(3,
                    RoleNames.Manager,
                    new List<Permission>
                    {
                        new(Permissions.EditFullEmployeesData),
                    }
                ),
            new Role(4, RoleNames.Employee, new List<Permission>()),
        };
    }
}