using Accounts.Application.Users.Queries;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Accounts.Tests.TestsData;
using Moq;

namespace Accounts.Tests;

public class GetPermissionsByAccountIdQueryHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();

    [Fact]
    public async Task PermissionListCannotHasDuplicates()
    {
        var roles = new List<Role>
        {
            new Role(1,
                    TestData.RoleNames.Admin,
                    new List<Permission>
                    {
                        new(Permissions.EditFullEmployeesData),
                    }
                ),
            new Role(2,
                    TestData.RoleNames.Ceo,
                    new List<Permission>
                    {
                        new(Permissions.EditFullEmployeesData),
                        new(Permissions.AccessAnalyticalForecastsPage),
                    }
                ),
        };

        var account = new Account("test@tourmalinecore.com",
                "test",
                "test",
                "test",
                roles
            );

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var queryHandler = new GetPermissionsByAccountIdQueryHandler(_accountRepositoryMock.Object);

        var permissions = await queryHandler.Handle(It.IsAny<long>());
        permissions = permissions.ToList();

        Assert.Equal(2, permissions.Count());
        Assert.Contains(Permissions.EditFullEmployeesData, permissions);
        Assert.Contains(Permissions.AccessAnalyticalForecastsPage, permissions);
    }
}