using Core.Entities;
using Core.Exceptions;
using Core.Models;

namespace Tests.Accounts;

public class AccountTests
{
    [Fact]
    public void CannotBlockMyself()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        var exception = Assert.Throws<AccountBlockingException>(() => account.Block("test@tourmalinecore.com"));
        Assert.Equal("Can't block myself", exception.Message);
    }

    [Fact]
    public void CannotBlockAdmin()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>
                {
                    new Role(1, BaseRoleNames.Admin, new List<Permission>()),
                }
            );

        var exception = Assert.Throws<AccountBlockingException>(() => account.Block("test@tourmalinecore.com"));
        Assert.Equal("Admin can't be blocked", exception.Message);
    }

    [Fact]
    public void CannotUnblockMyself()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        account.Block("caller@tourmalinecore.com");

        var exception = Assert.Throws<AccountUnblockingException>(() => account.Unblock("test@tourmalinecore.com"));
        Assert.Equal("Can't unblock myself", exception.Message);
    }
}