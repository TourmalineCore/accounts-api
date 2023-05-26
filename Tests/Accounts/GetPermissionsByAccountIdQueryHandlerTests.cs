using Application.Accounts.Queries;
using Core.Contracts;
using Core.Entities;
using Core.Models;
using Moq;
using Tests.TestsData;

namespace Tests.Accounts;

public class GetPermissionsByAccountIdQueryHandlerTests
{
    private readonly Mock<IAccountsRepository> _accountRepositoryMock = new();

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

        var query = new GetPermissionsByAccountIdQuery
        {
            Id = It.IsAny<long>(),
        };

        _accountRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(account);

        var queryHandler = new GetPermissionsByAccountIdQueryHandler(_accountRepositoryMock.Object);

        var permissions = await queryHandler.HandleAsync(query);
        permissions = permissions.ToList();

        Assert.Equal(2, permissions.Count());
        Assert.Contains(Permissions.EditFullEmployeesData, permissions);
        Assert.Contains(Permissions.AccessAnalyticalForecastsPage, permissions);
    }
}