using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using Core.Exceptions;
using Core.Models;
using NodaTime;

namespace Core.Entities;

public class Account : IEntity
{
    public long Id { get; private set; }

    public string CorporateEmail { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? MiddleName { get; private set; }

    public bool IsBlocked { get; private set; }

    public Instant CreatedAt { get; init; }

    public List<AccountRole> AccountRoles { get; private set; } = new();

    public Instant? DeletedAtUtc { get; private set; }

    public Account(string corporateEmail, string firstName, string lastName, string? middleName, IEnumerable<Role> roles)
    {
        CorporateEmail = corporateEmail;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        IsBlocked = false;
        CreatedAt = SystemClock.Instance.GetCurrentInstant();

        AccountRoles = roles
            .Select(role => new AccountRole
                    {
                        RoleId = role.Id,
                        Role = role,
                    }
                )
            .ToList();
    }

    public void Update(string firstName, string lastName, string? middleName, IEnumerable<long> roleIds, string callerCorporateEmail)
    {
        if (IsAdmin())
        {
            throw new AccountOperationException("Admin can't be edited");
        }

        if (CorporateEmail == callerCorporateEmail)
        {
            throw new AccountOperationException("Can't edit myself");
        }

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        AccountRoles = roleIds
            .Select(roleId => new AccountRole
                    {
                        RoleId = roleId,
                    }
                )
            .ToList();
    }

    public void Block(string callerCorporateEmail)
    {
        if (IsAdmin())
        {
            throw new AccountOperationException("Admin can't be blocked");
        }

        if (CorporateEmail == callerCorporateEmail)
        {
            throw new AccountOperationException("Can't block myself");
        }

        IsBlocked = true;
    }

    public void Unblock(string callerCorporateEmail)
    {
        if (IsAdmin())
        {
            throw new AccountOperationException("Admin can't be unblocked");
        }

        if (CorporateEmail == callerCorporateEmail)
        {
            throw new AccountOperationException("Can't unblock myself");
        }

        IsBlocked = false;
    }

    private bool IsAdmin()
    {
        if (AccountRoles.Count == 0)
        {
            return false;
        }

        return AccountRoles
            .Select(x => x.Role.Name)
            .Contains(BaseRoleNames.Admin);
    }

    private Account()
    {
    }
}