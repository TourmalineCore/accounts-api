using Core.Entities;
using Core.Exceptions;
using Core.Models;
using DataAccess;
using DataAccess.Repositories;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class AdminAccountTests
{
    private readonly Account _adminAccount = new("inner-circle-admin@tourmalinecore.com",
            "Admin",
            "Admin",
            "Admin",
            new List<Role>
            {
                new(1, BaseRoleNames.Admin, TestData.ValidPermissions),
            }
        );

    private readonly AccountsRepository _accountsRepository;

    public AdminAccountTests()
    {
        _accountsRepository = new AccountsRepository(new Mock<AccountsDbContext>().Object);
    }

    [Fact]
    public async Task CannotCreateOneMoreAdmin()
    {
        var exception = await Assert.ThrowsAsync<AccountOperationException>(() => _accountsRepository.CreateAsync(_adminAccount));
        Assert.Equal("Can't create one more admin", exception.Message);
    }

    [Fact]
    public async Task CannotEditAdmin()
    {
        var exception = await Assert.ThrowsAsync<AccountOperationException>(() => _accountsRepository.UpdateAsync(_adminAccount));
        Assert.Equal("Can't edit admin", exception.Message);
    }

    [Fact]
    public async Task CannotRemoveAdmin()
    {
        var exception = await Assert.ThrowsAsync<AccountOperationException>(() => _accountsRepository.RemoveAsync(_adminAccount));
        Assert.Equal("Can't remove admin", exception.Message);
    }
}