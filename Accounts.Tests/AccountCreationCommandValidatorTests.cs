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
        
        private static class RoleNames
        {
            public const string Admin = "Admin";
            public const string Ceo = "Ceo";
            public const string Manager = "Manager";
            public const string Employee = "Employee";
        }

        private readonly List<Role> _roles = new()
        {
            new Role(1,
                    RoleNames.Admin,
                    new List<Permission>
                    {
                        new(Permissions.CanManageEmployees),
                    }
                ),
            new Role(2,
                    RoleNames.Ceo,
                    new List<Permission>
                    {
                        new(Permissions.CanManageEmployees),
                        new(Permissions.CanViewAnalytic),
                        new(Permissions.CanViewFinanceForPayroll),
                    }
                ),
            new Role(3,
                    RoleNames.Manager,
                    new List<Permission>
                    {
                        new(Permissions.CanManageEmployees),
                    }
                ),
            new Role(4, RoleNames.Employee, new List<Permission>()),
        };

        public AccountCreationCommandValidatorTests()
        {
            var accountValidOptionsMock = new Mock<IOptions<AccountValidationOptions>>();

            accountValidOptionsMock.Setup(x => x.Value)
                .Returns(new AccountValidationOptions
                        {
                            CorporateEmailDomain = "@tourmalinecore.com",
                        }
                    );
            var roleRepositoryMock = new Mock<IRoleRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();

            roleRepositoryMock
                .Setup(x => x.GetRolesAsync())
                .ReturnsAsync(_roles);

            _validator = new AccountCreationCommandValidator(
                    roleRepositoryMock.Object,
                    _accountRepositoryMock.Object,
                    accountValidOptionsMock.Object
                );
        }

        [Fact]
        public async Task AllParamsAreValid_ReturnTrue()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long>
                {
                    1,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task CorporateEmailAlreadyExists_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long>
                {
                    3,
                },
            };

            _accountRepositoryMock
                .Setup(x => x.FindByCorporateEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Account("ivan@tourmalinecore.com",
                            "Ivan",
                            "Smith",
                            "Alexandrovich",
                            new List<Role>
                            {
                                new(RoleNames.Employee),
                            }
                        )
                    );

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task ParamsAreEmpty_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "",
                LastName = "",
                CorporateEmail = "",
                RoleIds = new List<long>
                {
                    1,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task CorporateEmailIsInvalid_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "John",
                LastName = "Doe",
                CorporateEmail = "invalidmail.com",
                RoleIds = new List<long>
                {
                    1,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task RoleIdIsNegative_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long>
                {
                    -1,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task RoleIdIsNotExist_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long>
                {
                    100,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task RoleIdsHaveDuplicates_ReturnFalse()
        {
            var accountCreationCommand = new AccountCreationCommand
            {
                FirstName = "Ivan",
                LastName = "Smith",
                CorporateEmail = "ivan@tourmalinecore.com",
                RoleIds = new List<long>
                {
                    1,
                    1,
                    1,
                    1,
                },
            };

            var validationResult = await _validator.ValidateAsync(accountCreationCommand);
            Assert.False(validationResult.IsValid);
        }
    }
}