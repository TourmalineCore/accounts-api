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

        var exception = Assert.Throws<AccountOperationException>(() => account.Block("test@tourmalinecore.com"));
        Assert.Equal("The operation cannot be executed with own account", exception.Message);
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

        var exception = Assert.Throws<AccountOperationException>(() => account.Unblock("test@tourmalinecore.com"));
        Assert.Equal("The operation cannot be executed with own account", exception.Message);
    }

    [Fact]
    public void CannotEditMyself()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        var exception = Assert.Throws<AccountOperationException>(() => account.Update("firstName",
                    "lastName",
                    "middleName",
                    new List<Role>
                    {
                        new Role(BaseRoleNames.Ceo, new List<Permission>()),
                    },
                    "test@tourmalinecore.com"
                )
            );

        Assert.Equal("The operation cannot be executed with own account", exception.Message);
    }

    [Fact]
    public void CannotSetAdminRoleForAccount()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        var exception = Assert.Throws<AccountOperationException>(() => account.Update("test",
                    "test",
                    "test",
                    new List<Role>
                    {
                        new Role(BaseRoleNames.Admin, new List<Permission>()),
                    },
                    "admin@tourmalinecore.com"
                )
            );

        Assert.Equal("Can't set admin role", exception.Message);
    }
}