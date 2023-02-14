using Accounts.Api.Controllers;
using Accounts.Application.HttpClients;
using Accounts.Application.Users;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;

namespace Accounts.Tests
{
    public class AccountCreateTest
    {
        private readonly List<Account> _accounts = new List<Account>()
        {
            new Account
            (
                "one@turmalineinnercore.com",
                "OneFirstName",
                "OneLastName",
                new List<Role>
                {
                    new Role(RoleNames.Admin)
                }
            )
        };
        private readonly AccountsController accountsController;
        public AccountCreateTest()
        {
            var iAccountRepositoryMock = new Mock<IAccountRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            var iValidatorMock = new Mock<IValidator<AccountCreationCommand>>();
            var iHttpClientMock = new Mock<IHttpClient>();
            var getAccountsQueryMock = new Mock<GetAccountsQuery>();


            var getAccountsQueryHandlerMock = new GetAccountsQueryHandler(iAccountRepositoryMock.Object);
            var getAccountByIdQueryHandlerMock = new GetAccountByIdQueryHandler(iAccountRepositoryMock.Object);
            var accountCreationCommandHandlerMock = new AccountCreationCommandHandler(
                iAccountRepositoryMock.Object,
                iValidatorMock.Object,
                iHttpClientMock.Object,
                iRoleRepositoryMock.Object
            );

            accountsController = new AccountsController
            (
                getAccountsQueryHandlerMock,
                accountCreationCommandHandlerMock,
                getAccountByIdQueryHandlerMock
            );
        }

        [Fact]
        public void Test1()
        {

            //var result = controller.FindAll(getAccountsQueryMock.Object);
            //var viewResult = Assert.IsType<Task<IEnumerable<AccountDto>>>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<AccountDto>>(viewResult.Result);
            //Assert.Equal(GetAccounts().Result.Count(), model.Count());
        }
        [Fact]
        public void TestCompletedCreating()
        {
            var account = new AccountCreationCommand
            {
                FirstName = "Павел",
                LastName = "Павлович",
                CorporateEmail = "pavel@tourmalineinnercore.com",
                RoleIds = new List<long>() { 1 }
            };

            var result = accountsController.CreateAsync(account).Result;
            Assert.NotEqual(0, result.Value);
        }
        [Fact]
        public void TestFaildEmptyFieldsCreating()
        {
            var account = new AccountCreationCommand
            {
                FirstName = "",
                LastName = "",
                CorporateEmail = "",
                RoleIds = new List<long>()
            };

            var result = accountsController.CreateAsync(account).Result;
            var viewResult = Assert.IsType<ActionResult<long>>(result);
            var model = Assert.IsAssignableFrom<ActionResult<long>>(viewResult);
            Assert.Equal(0, model.Value);

        }
        [Fact]
        public void TestFaildCreatExistingEmail()
        {
            var accountOne = new AccountCreationCommand
            {
                FirstName = "Павел",
                LastName = "Павлович",
                CorporateEmail = "pavel@tourmalineinnercore.com",
                RoleIds = new List<long>() { 1 }
            };
            var accountTwo = new AccountCreationCommand
            {
                FirstName = "Павел",
                LastName = "Павлович",
                CorporateEmail = "pavel@tourmalineinnercore.com",
                RoleIds = new List<long>() { 1 }
            };

            var result = accountsController.CreateAsync(accountTwo).Result;
            var viewResult = Assert.IsType<ActionResult<long>>(result);
            var model = Assert.IsAssignableFrom<ActionResult<long>>(viewResult);
            Assert.Equal(0, model.Value);
        }
    }
}