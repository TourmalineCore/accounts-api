using Application.Accounts.Commands;
using Application.HttpClients;
using Core.Contracts;
using Core.Entities;
using Core.Models;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class AccountBlockCommandHandlerTests
{
    private readonly Mock<IAccountsRepository> _accountRepositoryMock = new();
    private readonly Mock<IHttpClient> _httpClientMock = new();

    private readonly List<Role> _roles = new()
    {
        new Role(BaseRoleNames.Ceo, TestData.ValidPermissions),
    };

    public AccountBlockCommandHandlerTests()
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
                _roles
            );

        var command = new AccountBlockCommand
        {
            Id = It.IsAny<long>(),
        };

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var accountBlockCommandHandler = new AccountBlockCommandHandler(_accountRepositoryMock.Object, _httpClientMock.Object);

        Assert.False(account.IsBlocked);
        await accountBlockCommandHandler.HandleAsync(command);
        Assert.True(account.IsBlocked);
    }

    [Fact]
    public async Task CanBlockAccountMultipleTimes()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                _roles
            );

        var command = new AccountBlockCommand
        {
            Id = It.IsAny<long>(),
        };

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var accountBlockCommandHandler = new AccountBlockCommandHandler(_accountRepositoryMock.Object, _httpClientMock.Object);

        Assert.False(account.IsBlocked);
        await accountBlockCommandHandler.HandleAsync(command);
        Assert.True(account.IsBlocked);

        await accountBlockCommandHandler.HandleAsync(command);
        Assert.True(account.IsBlocked);
    }
}