using Application.Accounts.Commands;
using Application.Accounts.Validators;
using Application.Options;
using Core.Contracts;
using Core.Entities;
using Core.Models;
using Microsoft.Extensions.Options;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class AccountCreationCommandValidatorTests
{
    private readonly AccountCreationCommandValidator _validator;
    private readonly Mock<IAccountsRepository> _accountRepositoryMock;
    private readonly string _longString = new('a', 51);

    public AccountCreationCommandValidatorTests()
    {
        var accountValidOptionsMock = new Mock<IOptions<AccountValidationOptions>>();

        accountValidOptionsMock.Setup(x => x.Value)
            .Returns(new AccountValidationOptions
            {
                CorporateEmailDomain = "@tourmalinecore.com",
            }
                );
        var roleRepositoryMock = new Mock<IRolesRepository>();
        _accountRepositoryMock = new Mock<IAccountsRepository>();

        roleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(TestData.AllRoles);

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
                        TestData.ValidAccountRoles,
                        1L
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

    [Fact]
    public async Task FirstNameLengthMoreThan50_ReturnFalse()
    {
        var accountCreationCommand = new AccountCreationCommand
        {
            FirstName = _longString,
            LastName = "Smith",
            CorporateEmail = "ivan@tourmalinecore.com",
            RoleIds = new List<long>
            {
                2,
            },
        };

        var validationResult = await _validator.ValidateAsync(accountCreationCommand);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task LastNameLengthMoreThan50_ReturnFalse()
    {
        var accountCreationCommand = new AccountCreationCommand
        {
            FirstName = "Ivan",
            LastName = _longString,
            CorporateEmail = "ivan@tourmalinecore.com",
            RoleIds = new List<long>
            {
                2,
            },
        };

        var validationResult = await _validator.ValidateAsync(accountCreationCommand);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task MiddleNameLengthMoreThan50_ReturnFalse()
    {
        var accountCreationCommand = new AccountCreationCommand
        {
            FirstName = "Ivan",
            LastName = "Smith",
            MiddleName = _longString,
            CorporateEmail = "ivan@tourmalinecore.com",
            RoleIds = new List<long>
            {
                2,
            },
        };

        var validationResult = await _validator.ValidateAsync(accountCreationCommand);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task RoleIdsAreEmpty_ReturnFalse()
    {
        var accountCreationCommand = new AccountCreationCommand
        {
            FirstName = "Ivan",
            LastName = "Smith",
            MiddleName = _longString,
            CorporateEmail = "ivan@tourmalinecore.com",
            RoleIds = new List<long>(),
        };

        var validationResult = await _validator.ValidateAsync(accountCreationCommand);
        Assert.False(validationResult.IsValid);
    }
}