using NodaTime;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Core.Entities
{
    public class Account : IIdentityEntity
    {
        public long Id { get; private set; }

        public string Email { get; private set; }

        public List<AccountRole> AccountRoles { get; private set; } = new List<AccountRole>();

        public Instant? DeletedAtUtc { get; private set; } = null;

        // For DB Context
        private Account() { }

        public Account(string email, List<Role> roles)
        {
            Email = email;
            AccountRoles = roles.Select(role => new AccountRole
            {
              Role = role
            }).ToList();
        }

        public void Update(string email, List<AccountRole> accountRoles)
        {
            Email = email;
            AccountRoles = accountRoles;
        }

        public void AddRole(AccountRole role)
        {
            AccountRoles.Add(role);
        }

        public void Delete(Instant deletedAtUtc)
        {
            DeletedAtUtc = deletedAtUtc;
        }
    }
}
