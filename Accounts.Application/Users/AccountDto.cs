using System;
using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Accounts.Application.Roles;

namespace Accounts.Application.Users
{
    public readonly struct AccountDto
    {
        public AccountDto(Account account)
        {
            Id = account.Id;
            CorporateEmail = account.CorporateEmail;
            FirstName = account.FirstName;
            LastName = account.LastName;
            Roles = account.AccountRoles.Select(x => new RoleDto(x.Role));
            // Refactor: use Instant in controllers instead of date time
            CreationDate = account.CreatedAt.ToDateTimeUtc();
        }

        public long Id { get; init; }

        public string CorporateEmail { get; init; }

        public DateTime CreationDate { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public IEnumerable<RoleDto> Roles { get; init; }
    }
}
