using Application.Roles.Commands;
using Core.Contracts;
using Core.Entities;
using Moq;
using Tests.TestsData;

namespace Tests;

public class RoleUpdateCommandTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock = new();
    private readonly Role _adminRole = TestData.Roles.Single(x => x.Name == TestData.RoleNames.Admin);

    public RoleUpdateCommandTests()
    {
        _roleRepositoryMock
            .Setup(x => x.GetRolesAsync())
            .ReturnsAsync(TestData.Roles);
    }

    [Fact]
    public async Task CanUpdateRoleWithTheSameParams()
    {
        var command = new RoleUpdateCommand
        {
            Id = _adminRole.Id,
            Name = _adminRole.Name,
            Permissions = _adminRole.Permissions.ToList(),
        };

        _roleRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_adminRole);

        var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
        var exception = await Record.ExceptionAsync(() => roleUpdateCommandHandler.Handle(command));

        Assert.Null(exception);
    }

    [Fact]
    public async Task CannotUpdateRoleIfNameIsNotUnique()
    {
        var command = new RoleUpdateCommand
        {
            Id = _adminRole.Id,
            Name = TestData.RoleNames.Ceo,
            Permissions = _adminRole.Permissions.ToList(),
        };

        _roleRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_adminRole);

        var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleUpdateCommandHandler.Handle(command));

        Assert.Equal($"Role with name [{TestData.RoleNames.Ceo}] already exists", exception.Message);
    }

    [Fact]
    public async Task CannotUpdateRoleWithNonExistingPermissions()
    {
        var command = new RoleUpdateCommand
        {
            Id = _adminRole.Id,
            Name = _adminRole.Name,
            Permissions = new List<string>
            {
                "NonExistingPermission",
            },
        };

        _roleRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_adminRole);

        var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleUpdateCommandHandler.Handle(command));

        Assert.Equal("Permission [NonExistingPermission] doesn't exists", exception.Message);
    }

    [Fact]
    public async Task CannotUpdateRoleIfDoesNotExists()
    {
        const int nonExistingRoleId = 1000;

        var command = new RoleUpdateCommand
        {
            Id = nonExistingRoleId,
            Name = _adminRole.Name,
            Permissions = _adminRole.Permissions.ToList(),
        };

        var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
        await Assert.ThrowsAsync<NullReferenceException>(() => roleUpdateCommandHandler.Handle(command));
    }
}