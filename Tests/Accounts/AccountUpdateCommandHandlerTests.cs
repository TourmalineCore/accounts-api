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
    private readonly string _longString = new('a', 51);

    private readonly Account _testAccount = new("test@tourmalinecore.com",
            "test",
            "test",
            "test",
            TestData.ValidAccountRoles,
            1L
        );

    private readonly long _ceoRoleId = TestData
        .AllRoles
        .Single(x => x.Name == TestData.RoleNames.Ceo)
        .Id;

    public AccountUpdateCommandHandlerTests()
    {
        _roleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(TestData.AllRoles);

        var ceoRole = TestData.AllRoles.Single(x => x.Name == TestData.RoleNames.Ceo);

        _roleRepositoryMock
            .Setup(x => x.FindAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(new List<Role>
                    {
                        ceoRole,
                    }
                );
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
                _ceoRoleId,
            },
        };

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
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

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
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
                _ceoRoleId,
                _ceoRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
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
                _ceoRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
        await accountUpdateCommandHandler.HandleAsync(command);

        var account = await _accountRepositoryMock.Object.FindByIdAsync(1);

        Assert.Equal(command.FirstName, account.FirstName);
        Assert.Equal(command.LastName, account.LastName);
        Assert.Equal(command.MiddleName, account.MiddleName);
        Assert.Single(account.AccountRoles);
        Assert.True(account.AccountRoles.Exists(x => x.RoleId == _ceoRoleId));
    }

    [Fact]
    public async Task CannotUpdateAccountIfFirstNameLengthMoreThan50()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = _longString,
            LastName = "last name",
            Roles = new List<long>
            {
                _ceoRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
        await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));
    }

    [Fact]
    public async Task CannotUpdateAccountIfLastNameLengthMoreThan50()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = _longString,
            Roles = new List<long>
            {
                _ceoRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
        await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));
    }

    [Fact]
    public async Task CannotUpdateAccountIfMiddleNameLengthMoreThan50()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "middle name",
            MiddleName = _longString,
            Roles = new List<long>
            {
                _ceoRoleId,
            },
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
        await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));
    }

    [Fact]
    public async Task CannotUpdateAccountIfRolesAreEmpty()
    {
        var command = new AccountUpdateCommand
        {
            Id = 1,
            FirstName = "first name",
            LastName = "middle name",
            MiddleName = _longString,
            Roles = new List<long>(),
        };

        _accountRepositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_testAccount);

        var accountUpdateCommandHandler = new AccountUpdateCommandHandler(_accountRepositoryMock.Object, new AccountUpdateCommandValidator(_roleRepositoryMock.Object), _roleRepositoryMock.Object);
        await Assert.ThrowsAsync<ValidationException>(() => accountUpdateCommandHandler.HandleAsync(command));
    }
}