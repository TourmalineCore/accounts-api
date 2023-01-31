using System.Collections.Generic;

namespace Accounts.Core.Entities
{
    public enum RoleNames
    {
        Admin = 1,
        CEO,
        Manager,
        Employee
    }

    public class Role : IIdentityEntity
    {
        public long Id { get; private set; }

        public RoleNames Name { get; private set; }

        public string NormalizedName { get; private set; }

        public List<AccountRole> AccountRoles { get; private set; }

        public List<Privilege> Privileges { get; private set; } = new List<Privilege>();

        // For Db Context
        private Role() { }

        public Role(RoleNames name)
        {
            Name = name;
            NormalizedName = name.ToString().Normalize();

        }

        public Role(long id, RoleNames name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToString().Normalize();
        }

        public void Update(RoleNames name)
        {
            Name = name;
            NormalizedName = name.ToString().Normalize();
        }
        public void UpdateRole(List<Privilege> privileges)
        {
            Privileges = privileges;
        }
    }
}
