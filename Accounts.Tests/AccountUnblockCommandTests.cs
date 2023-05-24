using Accounts.Application.HttpClients;
using Accounts.Application.Users.Commands;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Moq;

namespace Accounts.Tests;

public class AccountUnblockCommandTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IHttpClient> _httpClientMock = new();

    public AccountUnblockCommandTests()
    {
        _httpClientMock
            .Setup(x => x.SendRequestToUnblockUserAsync(It.IsAny<long>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task CanUnblockAccount()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        account.Block();

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var command = new AccountUnblockCommand(_accountRepositoryMock.Object, _httpClientMock.Object);

        await command.Handle(It.IsAny<long>());
        Assert.False(account.IsBlocked);
    }

    [Fact]
    public async Task CanUnblockAccountMultipleTimes()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        account.Block();

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var command = new AccountUnblockCommand(_accountRepositoryMock.Object, _httpClientMock.Object);

        await command.Handle(It.IsAny<long>());
        Assert.False(account.IsBlocked);

        await command.Handle(It.IsAny<long>());
        Assert.False(account.IsBlocked);
    }
}