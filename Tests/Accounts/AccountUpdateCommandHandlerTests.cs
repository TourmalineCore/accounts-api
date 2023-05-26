using Application.Accounts.Commands;
using Application.Accounts.Validators;
using Core.Contracts;
using Core.Entities;
using FluentValidation;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class AccountUpdateCommandHandlerTests
{
    private readonly Mock<IAccountsRepository> _accountRepositoryMock = new();
    private readonly Mock<IRolesRepository> _roleRepositoryMock = new();

    private readonly Account _testAccount = new("test@tourmalinecore.com",
            "test",
            "test",
            "test",
            new List<Role>()
        );

    private readonly long _adminRoleId = TestData
        .Roles
        .Single(x => x.Name == TestData.RoleNames.Admin)
        .Id;

    public AccountUpdateCommandHandlerTests()
    {
        _roleRepositoryMock
            .Setup(x => x.GetRolesAsync())
            .ReturnsAsync(TestData.Roles);
    }

    [Fact]
    public async Task CannotUpdateAccountIfNotFound()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "last name",
            Roles = new List<long>
            {
                _adminRoleId,
            },
        };

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object));
        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => accountUpdateCommandHandler.HandleAsync(command));

        Assert.Equal("Account not found", exception.Message);
    }

    [Fact]
    public async Task CannotUpdateAccountIfRoleIdNotExists()
    {
        const int nonExistingRoleId = 1000;

        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "last name",
            Roles = new List<long>
            {
                nonExistingRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object));
        var exception = await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));

        Assert.Equal("Incorrect role ids. Probably you tried to set unavailable role id", exception.Message);
    }

    [Fact]
    public async Task CannotUpdateAccountIfRoleIdsHaveDuplicates()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "last name",
            Roles = new List<long>
            {
                _adminRoleId,
                _adminRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object));
        await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));
    }

    [Fact]
    public async Task CanUpdateAccountIfAllParamsAreValid()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "last name",
            MiddleName = "middle name",
            Roles = new List<long>
            {
                _adminRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object));
        await accountUpdateCommandHandler.HandleAsync(command);

        var account = await _accountRepositoryMock.Object.FindByIdAsync(1);

        Assert.Equal(command.FirstName, account.FirstName);
        Assert.Equal(command.LastName, account.LastName);
        Assert.Equal(command.MiddleName, account.MiddleName);
        Assert.Single(account.AccountRoles);
        Assert.True(account.AccountRoles.Exists(x => x.RoleId == _adminRoleId));
    }
}