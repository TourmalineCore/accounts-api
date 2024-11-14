using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Accounts.Commands;
using Application.Accounts.Validators;
using Application.HttpClients;
using Core.Contracts;
using Core.Entities;
using Core.Models;
using FluentValidation;
using Moq;
using Tests.TestsData;
using Xunit;

namespace Tests.Accounts;

public class AccountDeletionCommandHandlerTests
{
    private readonly Mock<IAccountsRepository> _accountRepositoryMock = new();
    private readonly Mock<IRolesRepository> _roleRepositoryMock = new();
    private readonly Account _testAccount = new(
        "test@tourmalinecore.com",
        "test",
        "test",
        "test",
        TestData.ValidAccountRoles,
        1L);

    public AccountDeletionCommandHandlerTests()
    {
        _roleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(TestData.AllRoles);

        _accountRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Account> { _testAccount });

        _accountRepositoryMock
            .Setup(x => x.FindByCorporateEmailAsync(_testAccount.CorporateEmail))
            .ReturnsAsync(_testAccount);

        _accountRepositoryMock
            .Setup(x => x.DeleteAsync(_testAccount))
            .Callback(() => _accountRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Account>()));
    }

    [Fact]
    public async Task CanDeleteAccountIfItExist()
    {
        var command = new AccountDeletionCommand
        {
            CorporateEmail = _testAccount.CorporateEmail
        };

        var accountDeletionCommandHandler = new AccountDeletionCommandHandler(
            _accountRepositoryMock.Object,
            new Mock<IHttpClient>().Object);

        await accountDeletionCommandHandler.HandleAsync("token", command);

        var accounts = await _accountRepositoryMock.Object.GetAllAsync();
        Assert.DoesNotContain(accounts, a => a.CorporateEmail == _testAccount.CorporateEmail);
    }
}