using Accounts.Application.HttpClients;
using Accounts.Application.Users.Commands;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Moq;

namespace Accounts.Tests;

public class AccountBlockCommandTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IHttpClient> _httpClientMock = new();

    public AccountBlockCommandTests()
    {
        _httpClientMock
            .Setup(x => x.SendRequestToBlockUserAsync(It.IsAny<long>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task CanBlockAccount()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var command = new AccountBlockCommand(_accountRepositoryMock.Object, _httpClientMock.Object);

        Assert.False(account.IsBlocked);
        await command.Handle(It.IsAny<long>());
        Assert.True(account.IsBlocked);
    }

    [Fact]
    public async Task CanBlockAccountMultipleTimes()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                new List<Role>()
            );

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var command = new AccountBlockCommand(_accountRepositoryMock.Object, _httpClientMock.Object);

        Assert.False(account.IsBlocked);
        await command.Handle(It.IsAny<long>());
        Assert.True(account.IsBlocked);

        await command.Handle(It.IsAny<long>());
        Assert.True(account.IsBlocked);
    }
}