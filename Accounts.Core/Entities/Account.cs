using NodaTime;
using System.Collections.Generic;
using System.Linq;

namespace Accounts.Core.Entities
{
    public class Account : IIdentityEntity
    {
        public long Id { get; private set; }

        public string CorporateEmail { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Instant CreatedAt { get; init; }

        public List<AccountRole> AccountRoles { get; private set; } = new();

        public Instant? DeletedAtUtc { get; private set; }

        public Account(string corporateEmail, string firstName, string lastName, IEnumerable<Role> roles)
        {
            CorporateEmail = corporateEmail;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = SystemClock.Instance.GetCurrentInstant();
            AccountRoles = roles
                .Select(role => new AccountRole { RoleId = role.Id })
                .ToList();
        }

        public void Update(string email, List<AccountRole> accountRoles)
        {
            CorporateEmail = email;
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

        // For DB Context
        private Account() { }
    }
}
