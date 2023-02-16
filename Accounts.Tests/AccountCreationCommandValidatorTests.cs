using Accounts.Application.Options;
using Accounts.Application.Users.Commands;
using Accounts.Application.Validators;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace Accounts.Tests
{
    public class AccountCreationCommandValidatorTests
    {
        private readonly AccountCreationCommandValidator _validator;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        private readonly List<Role> _roles = new()
        {
            new Role(1, RoleNames.Admin),
            new Role(2, RoleNames.CEO),
            new Role(3, RoleNames.Manager),
            new Role(4, RoleNames.Employee),
        };

        public AccountCreationCommandValidatorTests()
        {
            var accountValidOptionsMock = new Mock<IOptions<AccountValidOptions>>();
            accountValidOptionsMock.Setup(x => x.Value).Returns(new AccountValidOptions { ValidCorporateEmailDomain = "@tourmalinecore.com" });
            var roleRepositoryMock = new Mock<IRoleRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();

            roleRepositoryMock
                .Setup(x => x.GetRolesAsync())
                .ReturnsAsync(_roles);

            _validator = new AccountCreationCommandValidator
            (
                roleRepositoryMock.Object,
                _accountRepositoryMock.Object,
                accountValidOptionsMock.Object
            );
        }

        [Fact]
        public async Task ShouldSuccessfullyValidateNewAccountIfAllParamsIsValid()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long> { 1 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task CantCreateAccountWithExistingCorporateEmail()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long> { 3 }
            };

            _accountRepositoryMock
                .Setup(x => x.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Account("ivan@tourmalinecore.com", "Ivan", "Smith", new List<Role> { new(RoleNames.Employee) }));

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task CantCreateAccountWithEmptyFields()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "",
                LastName = "",
                CorporateEmail = "",
                RoleIds = new List<long> { 1 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }
        [Fact]
        public async Task CantCreateAccountIfCorporateEmailIsInvalid()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "John",
                LastName = "Doe",
                CorporateEmail = "invalidmail.com",
                RoleIds = new List<long> { 1 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task CantCreateAccountWithNegativeRoleId()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long> { -1 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task CantCreateAccountWithNonExistingRoleId()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long> { 100 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task CantCreateAccountIfRoleIdsHaveDuplicates()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long> { 1, 1, 1, 1 }
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }
    }
}