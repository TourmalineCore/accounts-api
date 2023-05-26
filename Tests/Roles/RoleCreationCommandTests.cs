using Application.Roles.Commands;
using Core.Contracts;
using Core.Models;
using Moq;
using Tests.TestsData;

namespace Tests.Roles;

public class RoleCreationCommandTests
{
    private readonly Mock<IRolesRepository> _roleRepositoryMock = new();

    public RoleCreationCommandTests()
    {
        _roleRepositoryMock
            .Setup(x => x.GetRolesAsync())
            .ReturnsAsync(TestData.Roles);
    }

    [Fact]
    public async Task CannotCreateRoleIfNameIsNotUnique()
    {
        var command = new RoleCreationCommand
        {
            Name = TestData.RoleNames.Admin,
            Permissions = new List<string>
            {
                Permissions.AccessAnalyticalForecastsPage,
            },
        };

        var roleCreationCommandHandler = new RoleCreationCommandHandler(_roleRepositoryMock.Object);
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleCreationCommandHandler.HandleAsync(command));

        Assert.Equal("Role with name [Admin] already exists", exception.Message);
    }

    [Fact]
    public async Task CannotCreateRoleWithNonExistingPermissions()
    {
        var command = new RoleCreationCommand
        {
            Name = "New role",
            Permissions = new List<string>
            {
                "NonExistingPermission",
            },
        };

        var roleCreationCommandHandler = new RoleCreationCommandHandler(_roleRepositoryMock.Object);
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleCreationCommandHandler.HandleAsync(command));

        Assert.Equal("Permission [NonExistingPermission] doesn't exists", exception.Message);
    }
}