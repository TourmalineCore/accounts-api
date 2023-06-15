using Core.Entities;
using Core.Exceptions;
using Core.Models;
using Tests.TestsData;

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
                TestData.ValidAccountRoles
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
                TestData.ValidAccountRoles
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
                TestData.ValidAccountRoles
            );

        var exception = Assert.Throws<AccountOperationException>(() => account.Update("firstName",
                    "lastName",
                    "middleName",
                    TestData.ValidAccountRoles,
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
                TestData.ValidAccountRoles
            );

        var exception = Assert.Throws<AccountOperationException>(() => account.Update("test",
                    "test",
                    "test",
                    new List<Role>
                    {
                        new Role(BaseRoleNames.Admin, TestData.ValidPermissions),
                    },
                    "admin@tourmalinecore.com"
                )
            );

        Assert.Equal("Can't set admin role", exception.Message);
    }

    [Fact]
    public void CannotCreateAccountWithEmptyRoles()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Account("test@tourmalinecore.com",
                    "test",
                    "test",
                    "test",
                    new List<Role>()
                )
            );

        Assert.Equal("Account roles can't be empty", exception.Message);
    }

    [Fact]
    public void CannotUpdateAccountIfNewRolesAreEmpty()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                TestData.ValidAccountRoles
            );

        var exception = Assert.Throws<ArgumentException>(() => account.Update("test",
                    "test",
                    "test",
                    new List<Role>(),
                    "user@tourmalinecore.com"
                )
            );

        Assert.Equal("Account roles can't be empty", exception.Message);
    }
}