using Accounts.Api.Controllers;
using Accounts.Application.HttpClients;
using Accounts.Application.Users;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Application.Validators;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Accounts.DataAccess;
using Accounts.DataAccess.Respositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Accounts.Tests
{
    public class AccountCreateTest
    {
        private readonly AccountCreationCommandValidator iValidatorMock;
        private readonly Mock<IAccountRepository> iAccountRepository;
        private readonly Mock<IRoleRepository> iRoleRepository;
        private readonly List<Account> accounts = new List<Account>()
        {
            new Account
            (
                "pavel@tourmalineinner.com",
                "Павел",
                "Павлович",
                new List<Role>()
                {
                    new Role(RoleNames.Employee)
                }
            ),
            new Account
            (
                "anton@tourmalineinner.com",
                "Антон",
                "Антонов",
                new List<Role>()
                {
                    new Role(RoleNames.Employee)
                }
            ),
        };
        private readonly List<Role> roles = new List<Role>()
        {
            new Role(3, RoleNames.Manager),
            new Role(4, RoleNames.Employee),
            new Role(2, RoleNames.CEO),
            new Role(1, RoleNames.Admin),
        };
        public AccountCreateTest()
        {
            iRoleRepository = new Mock<IRoleRepository>();
            iAccountRepository = new Mock<IAccountRepository>();
            iAccountRepository.Setup(el => (el.GetAllAsync()).Result).Returns(accounts);
            iRoleRepository.Setup(el => el.GetRolesAsync())
                .ReturnsAsync(roles);

            iValidatorMock = new AccountCreationCommandValidator
            (
                iRoleRepository.Object,
                iAccountRepository.Object
            );
        }
        [Fact]
        public void TestCompleted_ValidationIsEnabled()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Антон",
                LastName = "Антонов",
                CorporateEmail = "",
                RoleIds = new List<long>() { -1 }
            };

            var iHttpClientMock = new Mock<IHttpClient>();

            var accountCreationCommandHandler = new AccountCreationCommandHandler(
                iAccountRepository.Object,
                iValidatorMock,
                iHttpClientMock.Object,
                iRoleRepository.Object
            );

            var res = accountCreationCommandHandler.HandleAsync(accountCreate).Exception;
            try
            {
                Assert.Contains("Corporate Email", res.Message);
            }
            catch(NullReferenceException ex)
            {
                throw new InvalidOperationException("Validation is not enabled");
            }

        }
        [Fact]
        public void TestCompleted_GetAll()
        {
            var res = iAccountRepository.Object.GetAllAsync();
            Assert.Equal(accounts.Count, res.Result.Count());
        }
        [Fact]
        public void TestCompleted_GetByCorporateEmailAsync()
        {
            var account = new Account
            (
                "anton@tourmalineinner.com",
                "Антон",
                "Антонов",
                new List<Role>()
                {
                    new Role(RoleNames.Employee)
                }
            );

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.Single(el => el.CorporateEmail == account.CorporateEmail));

            var res = iAccountRepository.Object.FindByCorporateEmailAsync("pavel@tourmalineinner.com");

            Assert.Equal(account.CorporateEmail, res.Result.CorporateEmail);
            Assert.Equal(account.FirstName, res.Result.FirstName);
            Assert.Equal(account.LastName, res.Result.LastName);
        }
        [Fact]
        public void TestCompleted_CreateAccount()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Костя",
                LastName = "Костянов",
                CorporateEmail = "kostya@tourmalineinner.com",
                RoleIds = new List<long>() { 1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.Equal(true, validationResult.Result.IsValid);
        }
        [Fact]
        public void TestField_CreateAccountTwiceSameEmail()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Антон",
                LastName = "Антонов",
                CorporateEmail = "pavel@tourmalineinner.com",
                RoleIds = new List<long>() { 1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.NotEqual(true, validationResult.Result.IsValid);
        }
        [Fact]
        public void TestField_CreateAccountEmptyFields()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "",
                LastName = "",
                CorporateEmail = "",
                RoleIds = new List<long>() { 1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.NotEqual(true, validationResult.Result.IsValid);
        }
        [Fact]
        public void TestField_CreateAccountIncorrectEmailFormat()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Антон",
                LastName = "Антонов",
                CorporateEmail = "trolmail.ru",
                RoleIds = new List<long>() { 1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.NotEqual(true, validationResult.Result.IsValid);
        }
        [Fact]
        public void TestField_CreateUserRoleIdDoesNotExist()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Костя",
                LastName = "Костянов",
                CorporateEmail = "kostya@mail.ru",
                RoleIds = new List<long>() { -1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.NotEqual(true, validationResult.Result.IsValid);
        }
        [Fact]
        public void TestField_CreateUserNegativeRoleId()
        {
            var accountCreate = new AccountCreationCommand
            {
                FirstName = "Костя",
                LastName = "Костянов",
                CorporateEmail = "kostya@mail.ru",
                RoleIds = new List<long>() { 1, 1, 1, 1 }
            };

            iAccountRepository.Setup(el => el.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(accounts.SingleOrDefault(el => el.CorporateEmail == accountCreate.CorporateEmail));

            var validationResult = iValidatorMock.ValidateAsync(accountCreate);
            Assert.NotEqual(true, validationResult.Result.IsValid);
        }
    }
}