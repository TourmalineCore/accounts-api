using Application.Accounts.Commands;
using Application.HttpClients;
using Core.Contracts;
using Core.Entities;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class AccountUnblockCommandHandlerTests
{
    private readonly Mock<IAccountsRepository> _accountRepositoryMock = new();
    private readonly Mock<IHttpClient> _httpClientMock = new();

    public AccountUnblockCommandHandlerTests()
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
                TestData.ValidAccountRoles
            );

        var command = new AccountUnblockCommand
        {
            Id = It.IsAny<long>(),
        };

        account.Block("caller@tourmalinecore.com");

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var accountUnblockCommandHandler = new AccountUnblockCommandHandler(_accountRepositoryMock.Object, _httpClientMock.Object);

        await accountUnblockCommandHandler.HandleAsync(command);
        Assert.False(account.IsBlocked);
    }

    [Fact]
    public async Task CanUnblockAccountMultipleTimes()
    {
        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                TestData.ValidAccountRoles
            );

        var command = new AccountUnblockCommand
        {
            Id = It.IsAny<long>(),
        };

        account.Block("caller@tourmalinecore.com");

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var accountUnblockCommandHandler = new AccountUnblockCommandHandler(_accountRepositoryMock.Object, _httpClientMock.Object);

        await accountUnblockCommandHandler.HandleAsync(command);
        Assert.False(account.IsBlocked);

        await accountUnblockCommandHandler.HandleAsync(command);
        Assert.False(account.IsBlocked);
    }
}